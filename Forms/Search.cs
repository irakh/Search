using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using search.Helpers;

namespace search.Forms
{
    public partial class Search : Form
    {
        // Объект для отмены операции поиска
        private CancellationTokenSource cancellationTokenSource;
        // Задача поиска
        private Task searchTask;
        // Таймер для отслеживания прошедшего времени
        public System.Threading.Timer timer;
        // Прошедшее время в секундах
        private int elapsedTime;

        public Search()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(SearchForm_FormClosing);
            elapsedTime = 0;
            // Кнопка "Остановить" неактивна при запуске
            stopButton.Enabled = false;
            // Кнопка "Продолжить" неактивна при запуске
            continueButton.Enabled = false;
            FileSearchApp.SearchCompleted += SearchCompletedHandler;
        }
        /// <summary>
        /// Обработчик завершения поиска
        /// </summary>
        private void SearchCompletedHandler(object sender, FileSearchEventArgs e)
        {
            // Обработка завершения поиска
            if (e.IsCompleted)
            {
                TimerStop();
                stopButton.Enabled = false;
                continueButton.Enabled = false;
                MessageBox.Show("Поиск завершен. Все файлы найдены.");
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки "Поиск"
        /// </summary>
        private async void SearchButton_Click(object sender, EventArgs e)
        {
            // Проверяем существование стартовой директории
            if (Directory.Exists(startDirectoryTextBox.Text))
            {
                if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
                {
                    cancellationTokenSource.Cancel();
                }
                fileTreeView.Nodes.Clear();
                foundFilesLabel.Text = "Найдено файлов: 0";
                totalFilesLabel.Text = "Всего файлов: 0";
                elapsedTimeLabel.Text = "Прошедшее время: 00:00:00";
                elapsedTime = 0;
                FileSearchApp.filesProcessed = 0;
                FileSearchApp.parentDirectory = startDirectoryTextBox.Text;
                cancellationTokenSource = new CancellationTokenSource();
                fileTreeView.Nodes.Clear();
                TimerStop();
                Timer();
                stopButton.Enabled = true;
                continueButton.Enabled = false;
                FileSearchApp.pauseEvent.Set(); 
                searchTask = Task.Run(() => FileSearchApp.SearchFiles(startDirectoryTextBox.Text, new Regex(filePatternTextBox.Text), cancellationTokenSource.Token, UpdateUI));
                await searchTask;
            }
            else
            {
                MessageBox.Show("Стартовая директория не существует.");
            }
        }
        /// <summary>
        /// Метод для запуска таймера
        /// </summary>
        public void Timer()
        {
            timer = new System.Threading.Timer(TimerCallback, null, 0, 1000);
        }
        /// <summary>
        /// Функция для таймера, отсчитывает секунды
        /// </summary>
        void TimerCallback(object state)
        {
            // Обновляем время на UI-потоке
            this.Invoke((MethodInvoker)delegate
            {
                elapsedTime++;
                elapsedTimeLabel.Text = $"Прошедшее время: {TimeSpan.FromSeconds(elapsedTime):hh\\:mm\\:ss}";
            });
        }
        /// <summary>
        /// Метод для остановки таймера
        /// </summary>
        public void TimerStop()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки "Остановить"
        /// </summary>
        private void StopButton_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource != null)
            {
                FileSearchApp.pauseEvent.Reset(); // Приостановить выполнение
                TimerStop();
                stopButton.Enabled = false;
                continueButton.Enabled = true;
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки "Продолжить"
        /// </summary>
        private void ContinueButton_Click(object sender, EventArgs e)
        {
            // Проверяем, установлено ли событие приостановки выполнения
            if (FileSearchApp.pauseEvent.IsSet == false) // Продолжить выполнение, если оно было приостановлено
            {
                FileSearchApp.pauseEvent.Set();
                Timer();
                stopButton.Enabled = true;
                continueButton.Enabled = false;
            }
        }
        /// <summary>
        /// Обработчик события загрузки формы
        /// </summary>
        private void SearchForm_Load(object sender, EventArgs e)
        {
            // Загружаем сохраненные настройки
            startDirectoryTextBox.Text = Properties.Settings.Default.StartDirectory;
            filePatternTextBox.Text = Properties.Settings.Default.FilePattern;
        }
        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void SearchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Сохраняем настройки при закрытии формы
            Properties.Settings.Default.StartDirectory = startDirectoryTextBox.Text;
            Properties.Settings.Default.FilePattern = filePatternTextBox.Text;
            Properties.Settings.Default.Save();
            if (searchTask != null && !searchTask.IsCompleted)
            {
                cancellationTokenSource.Cancel();
            }
            TimerStop();
            Environment.Exit(0);
        }
        /// <summary>
        /// Метод для обновления пользовательского интерфейса
        /// </summary>
        /// <param name="currentDirectory">Текущая директория</param>
        /// <param name="filePath">Найденный файл</param>
        /// <param name="foundFilesCount">Найдено файлов</param>
        /// <param name="totalFilesCount">Всего файлов</param>
        private void UpdateUI(string currentDirectory, string filePath, int foundFilesCount, int totalFilesCount)
        {
            // Обновляем метки и другую информацию на UI-потоке
            this.Invoke((MethodInvoker)delegate
            {
                currentDirectoryLabel.Text = $"Текущая директория: {currentDirectory}";
                if (!string.IsNullOrEmpty(filePath))
                {
                    AddFileToTreeView(filePath);
                }
                foundFilesLabel.Text = $"Найдено файлов: {foundFilesCount}";
                totalFilesLabel.Text = $"Всего файлов: {totalFilesCount}";
            });
        }
        /// <summary>
        /// Метод для добавления файла в дерево файлов
        /// </summary>
        private void AddFileToTreeView(string path)
        {
            var parts = path.Split(Path.DirectorySeparatorChar);
            TreeNodeCollection currentNodes = fileTreeView.Nodes;

            for (int i = 0; i < parts.Length; i++)
            {
                TreeNode foundNode = null;
                foreach (TreeNode node in currentNodes)
                {
                    if (node.Text == parts[i])
                    {
                        foundNode = node;
                        break;
                    }
                }

                if (foundNode == null)
                {
                    foundNode = new TreeNode(parts[i]);
                    currentNodes.Add(foundNode);
                }

                currentNodes = foundNode.Nodes;
            }
        }
    }
    class BufferedTreeView : TreeView
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);
            base.OnHandleCreated(e);
        }
        // Pinvoke:
        private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        private const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using search.Forms;

namespace search.Helpers
{
    public static class FileSearchApp 
    {
        // Событие, сигнализирующее о завершении поиска файлов
        public static event EventHandler<FileSearchEventArgs> SearchCompleted;
        public static int filesProcessed;
        public static string parentDirectory;
        /// <summary>
        /// Метод для вызова события завершения поиска файлов.
        /// </summary>
        /// <param name="isCompleted">Флаг, указывающий на завершение поиска.</param>
        private static void OnSearchCompleted(bool isCompleted)
        {
            SearchCompleted?.Invoke(null, new FileSearchEventArgs(isCompleted));
        }
        // Сигнальное событие для управления паузой поиска
        public static ManualResetEventSlim pauseEvent = new ManualResetEventSlim(true);
        /// <summary>
        /// Метод для запуска поиска файлов.
        /// </summary>
        /// <param name="startDirectory">Начальная директория для поиска.</param>
        /// <param name="pattern">Регулярное выражение для фильтрации файлов.</param>
        /// <param name="token">Токен отмены для отслеживания отмены операции.</param>
        /// <param name="updateUI">Метод обновления пользовательского интерфейса.</param>
        public static void SearchFiles(string startDirectory, Regex pattern, CancellationToken token, Action<string, string, int, int> updateUI)
        {
            try
            {
                int foundFilesCount = 0;
                int totalFilesCount = 0;
                SearchDirectory(new DirectoryInfo(startDirectory), pattern, token, pauseEvent, ref foundFilesCount, ref totalFilesCount, updateUI);
                OnSearchCompleted(true);
            }
            catch (OperationCanceledException)
            {
                // Обработка отмены операции
            }
        }
        /// <summary>
        /// Рекурсивный метод для поиска файлов в директории.
        /// </summary>
        /// <param name="directory">Директория, в которой выполняется поиск.</param>
        /// <param name="pattern">Регулярное выражение для фильтрации файлов.</param>
        /// <param name="token">Токен отмены для отслеживания отмены операции.</param>
        /// <param name="pauseEvent">Сигнальное событие для управления паузой поиска.</param>
        /// <param name="foundFilesCount">Количество найденных файлов.</param>
        /// <param name="totalFilesCount">Общее количество обработанных файлов.</param>
        /// <param name="updateUI">Метод обновления пользовательского интерфейса.</param>
        /// <returns>Возвращает true, если поиск завершен, иначе - false.</returns>
        private static bool SearchDirectory(DirectoryInfo directory, Regex pattern, CancellationToken token, ManualResetEventSlim pauseEvent, ref int foundFilesCount, ref int totalFilesCount, Action<string, string, int, int> updateUI)
        {
            token.ThrowIfCancellationRequested(); // Проверяем, не запрошена ли отмена операции
            pauseEvent.Wait(); // Проверяем паузу поиска
            try
            {
                updateUI(directory.FullName, null, foundFilesCount, totalFilesCount);

                // Перебираем файлы в директории
                foreach (var file in directory.GetFiles())
                {
                    token.ThrowIfCancellationRequested();
                    pauseEvent.Wait(); // Проверка паузы
                    totalFilesCount++;
                    if (pattern.IsMatch(file.Name)) // Проверяем, соответствует ли файл заданному шаблону
                    {
                        foundFilesCount++;
                        updateUI(directory.FullName, file.FullName, foundFilesCount, totalFilesCount);
                    }
                    filesProcessed++;
                }

                // Рекурсивно обрабатываем поддиректории
                foreach (var subDirectory in directory.GetDirectories())
                {
                    SearchDirectory(subDirectory, pattern, token, pauseEvent, ref foundFilesCount, ref totalFilesCount, updateUI);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Игнорировать директории и файлы, к которым нет доступа
            }
            catch (DirectoryNotFoundException)
            {
                // Игнорировать удалённые директории
            }
            // Если это корневая директория, проверяем, завершен ли поиск
            if (directory.Parent == null)
            {
                // Проверяем, что все файлы в текущей директории были обработаны
                bool isCompleted = filesProcessed == directory.GetFiles().Length;
                if (isCompleted)
                    OnSearchCompleted(true);// Сигнализируем о завершении поиска

                return isCompleted;
            }

            return true;
        }
    }
}

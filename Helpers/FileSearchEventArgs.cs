using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace search.Helpers
{
    public class FileSearchEventArgs : EventArgs
    {
        /// <summary>
        /// Получает значение, указывающее, завершен ли поиск файлов.
        /// </summary>
        public bool IsCompleted { get; private set; }
        /// <summary>
        /// Инициализирует новый экземпляр класса FileSearchEventArgs с указанием завершения поиска.
        /// </summary>
        /// <param name="isCompleted"></param>
        public FileSearchEventArgs(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }
    }
}

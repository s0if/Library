using Library.APPLICATION.Service;
using Library.DOMAIN.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Book
{
    public class DeleteBookUseCase
    {
        private readonly IBookInterface _bookInterface;
        private readonly UploadFile _uploadFile;

        public DeleteBookUseCase(IBookInterface bookInterface,UploadFile uploadFile)
        {
            _bookInterface = bookInterface;
            _uploadFile = uploadFile;
        }
        public async Task Execute(Guid Id)
        {
            var book = await _bookInterface.GetById(Id);
            if (book is null)
            {
                throw new Exception("Book not found");
            }
            var deleteFile = await _uploadFile.DeleteFileAsync(book.BookFile);
            if (!deleteFile)
            {
                throw new Exception("Failed to delete book file from storage");
            }
            await _bookInterface.Delete(Id); 
        }
    }
}

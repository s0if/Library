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

        public DeleteBookUseCase(IBookInterface bookInterface)
        {
            _bookInterface = bookInterface;
        }
        public async Task Execute(Guid Id)
        {
            await _bookInterface.Delete(Id); 
        }
    }
}

using Library.APPLICATION.DTO.Auth;
using Library.APPLICATION.UseCase.Auth;
using Library.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Read
{
    public class GetAllReadsByBookUseCase
    {
        private readonly IReadInterface _readInterface;
        private readonly GetUserMap _getUserMap;

        public GetAllReadsByBookUseCase(IReadInterface readInterface, GetUserMap getUserMap)
        {
            _readInterface = readInterface;
            _getUserMap = getUserMap;
        }
        public async Task<IEnumerable<GetUserDTOs>> Execute(Guid BookId)
        {
            var users = await _readInterface.GetReadsByBook(BookId);
            return users.Select(u => _getUserMap.ToUser(u));
            //return await _readInterface.GetReadsByBook(BookId);
        }
    }
}

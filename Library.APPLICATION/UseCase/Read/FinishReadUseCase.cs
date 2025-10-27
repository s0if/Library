using Library.APPLICATION.Mapping;
using Library.APPLICATION.UseCase.Auth;
using Library.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Read
{
    public class FinishReadUseCase
    {
        private readonly IReadInterface _read;
        private readonly GetRoleUseCase _getRole;
        private readonly ReadMap _readMap;

        public FinishReadUseCase( IReadInterface read, GetRoleUseCase getRole,ReadMap readMap)
        {
            _read = read;
            _getRole = getRole;
            _readMap = readMap;
        }
        public async Task Execute(Guid ReadId,string UserId)
        {
            var result = await _read.GetReadById(ReadId);
            if (result is null)
            {
                throw new Exception("Read record not found");
            }
            var roles = await _getRole.Execute(UserId);
            if (!roles.Contains("admin") && !roles.Contains("staff")&& result.UserId != UserId)
            {
                throw new UnauthorizedAccessException("You are not authorized to finish this read");
            }
            var read = await _readMap.FinishRead(result);
            await _read.FinishRead(read);
        }
    }
}

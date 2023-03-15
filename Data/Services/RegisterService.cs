using Dapper;
using Data.Repositary;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IDbConnection _dbConnection;

        public RegisterService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlConnection");
            _dbConnection = new SqlConnection(connectionString);
        }

     
   
        public async Task<Account> AddAccount(Account Rs)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Email", Rs.Email);
            parameter.Add("@PasswordHash", Rs.PasswordHash);
            parameter.Add("@AccountType", Rs.AccountType);
            parameter.Add("@DisplayName", Rs.DisplayName);
            parameter.Add("@VerificationToken", Rs.VerificationToken);
            parameter.Add("@isVerified", Rs.IsVerified);
            parameter.Add("@VerifiedOn", Rs.VerifiedOn);

            var results = await _dbConnection.QueryAsync<Account>("AddAccount", parameter, commandType: CommandType.StoredProcedure);
            return results.SingleOrDefault();
        }
                
    }
}

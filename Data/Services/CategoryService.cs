using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Data.Repositary;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDbConnection _dbConnection;

        public CategoryService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlConnection");
            _dbConnection = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var results = await _dbConnection.QueryAsync<Category>("GetCategory", commandType: CommandType.StoredProcedure);
            return results;
        }
        public async Task<Category> GetCategoryByID(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryId", id);

            var results = await _dbConnection.QueryAsync<Category>("GetCategory", parameters, commandType: CommandType.StoredProcedure);
            return results.FirstOrDefault();
        }

        public async Task<Category> CreateCategory(Category category)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", category.Name);
            parameters.Add("@Description", category.Description);

            var results = await _dbConnection.QueryAsync<Category>("CreateCategory", parameters, commandType: CommandType.StoredProcedure);
            return results.SingleOrDefault();
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", category.Name);
            parameters.Add("@CategoryId", category.CategoryId); 
            parameters.Add("@Description", category.Description);

            var results = await _dbConnection.QueryAsync<Category>("UpdateCategory", parameters, commandType: CommandType.StoredProcedure);
            return results.SingleOrDefault();
        }

      
        public async Task<IEnumerable<Category>> DeleteCategory(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryId", id);

            var results = await _dbConnection.QueryAsync<Category>("DeleteCategory", parameters, commandType: CommandType.StoredProcedure);
            return results;
        }


    }
}

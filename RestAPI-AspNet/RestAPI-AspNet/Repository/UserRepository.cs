using Microsoft.EntityFrameworkCore;
using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Model;
using RestAPI_AspNet.Model.Context;
using System.Security.Cryptography;
using System.Text;

namespace RestAPI_AspNet.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        //Implementa o metodo que valida as credencias do User
        public User? ValidateCredentials(UserVO user)
        {


            //obten a senha criptografada 
            var pass = ComputeHash(user.Password, SHA256.Create());   

            var userDB = _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));

            return userDB;

        }

        //valida e retorna o usuario encontrado
        public User? ValidateCredentials(string userName)
        {
            var userDB = _context.Users.FirstOrDefault(u => (u.UserName == userName));          

            return userDB;
        }


        //revoga o token 
        public bool RevokeToken(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => (u.UserName == userName));

            if (user == null) return false;

            //seta o valo nulo no token 
            user.RefreshToken = null;

            _context.SaveChanges();

            return true;



        }




        //Atualiza as informações do usuario
        public User? RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;




            var result = _context.Users.SingleOrDefault(u => u.Id.Equals(user.Id));


            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
          
            return result;
            

        }

        //Calcula o hash 
        private string ComputeHash(string input, SHA256 sHA256)
        {

            //Converte a string input em um array de bytes usando a codificação UTF-8
            byte[] inputByte = Encoding.UTF8.GetBytes(input);


            //Calcula o hash dos bytes convertidos de input usando o algoritmo SHA256
            byte[] hashedBytes = sHA256.ComputeHash(inputByte);

            var builder = new StringBuilder();



            foreach(var item in hashedBytes)
            {
                builder.Append(item.ToString("x2"));
            }

            return builder.ToString(); ;
        }
    }
}

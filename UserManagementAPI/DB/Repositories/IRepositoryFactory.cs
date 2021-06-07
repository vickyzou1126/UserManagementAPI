using UserManagementAPI.DB;

namespace UserManagementAPI.DB.Repositories
{
    public interface IRepositoryFactory
    {
        IUserRepository userRepository { get; }
        IAccountRepository accountRepository { get; }
    }

    public class RepositoryFactory : IRepositoryFactory
    {
        protected ApplicationContext _context;
        public RepositoryFactory(ApplicationContext _context){
            this._context = _context;
        }

        private IUserRepository _userRepository = null;
        public IUserRepository userRepository {
            get
            {
                if(_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        private IAccountRepository _accountRepository = null;
        public IAccountRepository accountRepository
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_context,this);
                }
                return _accountRepository;
            }
        }
    }
}

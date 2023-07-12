namespace UsersAPI.Domain.Exceptions
{
    public class AccessDeniedExeption : Exception
    {
        public override string Message =>
            "Acesso negado.Usuário ou senha inválidos.";
    }
}

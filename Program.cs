namespace TesteLogAlteracaoClasse
{
    public class Pessoa
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string? Email { get; set; }

        public Pessoa(long id, string nome, int idade)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
        }

        public Pessoa(long id, string nome, int idade, string email)
        {
            Id = id;
            Nome = nome;
            Idade = idade;
            Email = email;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Verificar se existe diferença do cadastro do objeto!");

            var pessoaOld = new Pessoa(1, "Brenno", 25, "brenno.s97@gmail.com");
            var pessoaNew = new Pessoa(1, "Brenno Sciammarella", 25);
            
            var alteracao = VerificarAlteracoes(pessoaOld, pessoaNew);
            
            Console.WriteLine(alteracao);
        }

        public static string VerificarAlteracoes(Pessoa velha, Pessoa nova)
        {
            var retorno = $"Objeto alterado \n";
            retorno += $"Id: {nova.Id.ToString()} \n";
            retorno += "Alterações:\n";

            if (velha.GetType() != nova.GetType())
                throw new Exception("Tipos são diferentes");
            
            foreach (var property in velha.GetType().GetProperties())
            {
                var propriedadeIgual = nova.GetType().GetProperties().FirstOrDefault(x => x.Name == property.Name);

                if (propriedadeIgual == null)
                    continue;
                else if (property.GetValue(velha) == null && propriedadeIgual.GetValue(nova) != null)
                    retorno += $"{propriedadeIgual.Name}: {propriedadeIgual.GetValue(nova).ToString()} \n";
                else if (property.GetValue(velha) != null && propriedadeIgual.GetValue(nova) == null)
                    retorno += $"{propriedadeIgual.Name}: NULL \n";
                else if (property.GetValue(velha).ToString() != propriedadeIgual.GetValue(nova).ToString())
                    retorno += $"{propriedadeIgual.Name}: {propriedadeIgual.GetValue(nova)} \n";
                else
                    continue;
            }

            return retorno;
        }
    }
}
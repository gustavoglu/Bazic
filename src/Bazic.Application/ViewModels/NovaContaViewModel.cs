
using Flunt.Validations;
using System;

namespace Bazic.Application.ViewModels
{
    public class NovaContaViewModel : ViewModel
    {
        public string NomeCompleto { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string ConfirmacaoSenha { get; set; }

        public Guid Id_contaTipo { get; set; }

        public override void Validate()
        {
            Contract c = new Contract()
                .IsNotNullOrEmpty(NomeCompleto,"NomeCompleto","Obrigatório informar o Nome Completo")
                .IsNotNullOrEmpty(Email, "Email", "Obrigatório informar o Email")
                .IsNotNullOrEmpty(Senha, "Senha", "Obrigatório informar a Senha")
                .IsNotNullOrEmpty(ConfirmacaoSenha, "ConfirmacaoSenha", "Obrigatório informar a ConfirmacaoSenha")
                .AreEquals(ConfirmacaoSenha,Senha,"ConfirmacaoSenha","A Confirmação da Senha precisa ser igual a Senha")
                .IsEmail(Email,"Email","Informe um Email válido")
                .AreNotEquals(Id_contaTipo,Guid.Empty,"Id_contaTipo", "Obrigatório informar a Conta Tipo")
                .IsNotNull(Id_contaTipo,"Id_contaTipo", "Obrigatório informar a Conta Tipo");

            AddNotifications(c);
        }
    }
}

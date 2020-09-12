using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XGame.Domain.Resources;

namespace XGame.Domain.ValueObjects
{
    public class Email : Notifiable
    {
        protected Email()
        {

        }
        public Email(string endereco)
        {
            Endereco = endereco;

            new AddNotifications<Email>(this).IfNotEmail(x => x.Endereco, Message.X0_INVALIDO.ToFormat("E-mail"));
        }

        public string Endereco { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio1
{
    public interface IOperacaoBancaria
    {
        void Depositar(decimal valor);
        void Sacar(decimal valor);
    }

}

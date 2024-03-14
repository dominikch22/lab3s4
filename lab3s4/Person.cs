using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3s4
{
    class Person
    {
        private string _name;
        public string Name => _name;
        private string _surname;
        public string Surname => _surname;
        private DateTime _birthDate;
        public DateTime BirthDate => _birthDate;


        public Person(string name, string surname, DateTime birthDate) =>
            (_name, _surname, _birthDate) = (name, surname, birthDate);

        public override string ToString() => $"{Name}, {Surname}, {BirthDate}";

        public object this[string property] =>
            GetType().GetProperty(property)?.GetValue(this);

        
       

    }
}

using System;

namespace WpfApp1
{
    public class Student
    {
        private string name;
        private int age;
        private int credits;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public int Credits
        {
            get { return credits; }
            set { credits = value; }
        }

        public virtual string ShowInfo()
        {
            return $"Name: {Name}, Age: {Age}, Credits: {Credits}";
        }
        public virtual bool IsGraduated()
        {

            return Credits >= 30;
        }
    }


    public class GraduatedStudent : Student
    {
       
    }
}


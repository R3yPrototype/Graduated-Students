using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    //This projects is about creating a list of students and saving the graduated ones in a txt file.
    //There can be a maximum of 60 credits and a minimum of 30 to be graduated.

    //--------------------------------------------------------------------------------------------//
    //--------------------------------------------------------------------------------------------//

    //Message box
    public class CustomMessageBox
    {
        public static void Show(string message)
        {
            Window window = new Window
            {
                Title = "Message",
                Content = new TextBlock { Text = message },
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow
            };
            window.ShowDialog();
        }
    }

    public partial class MainWindow : Window
    {
        private readonly List<Student> Students = new List<Student>();
        private readonly List<GraduatedStudent> GraduatedStudents = new List<GraduatedStudent>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        //Save students in ListBox
        private void SaveStudentButton_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student();
            if (string.IsNullOrWhiteSpace(StudentName.Text))
            {
                CustomMessageBox.Show("Please enter a name.");
                return;
            }
            if (!char.IsUpper(StudentName.Text[0]))
            {
                CustomMessageBox.Show("Name must start with an uppercase letter.");
                return;
            }
            student.Name = StudentName.Text;
            //-------------------------------------------------------------------------------------------------//
            if (!int.TryParse(StudentAge.Text, out int age) || age < 18 || age > 120)
            {
                CustomMessageBox.Show("Invalid age input. Please enter a valid integer between 18 and 120.");
                return;
            }
            student.Age = age;
            //-------------------------------------------------------------------------------------------------//
            if (!int.TryParse(StudentCredits.Text, out int credits) || credits < 1 || credits > 60)
            {
                CustomMessageBox.Show("Invalid credits input. Please enter a valid integer between 1 and 60.");
                return;
            }
            student.Credits = credits;
            //-------------------------------------------------------------------------------------------------//
            Students.Add(student);
            //-------------------------------------------------------------------------------------------------//
            // Saving graduated students
            if (student.IsGraduated())
            {
                GraduatedStudents.Add(new GraduatedStudent
                {
                    Name = student.Name,
                    Age = student.Age,
                    Credits = student.Credits
                });
            }
            //-------------------------------------------------------------------------------------------------//
            StudentListTextBox.Text += $"Name: {student.Name}, Age: {student.Age}, Credits: {student.Credits}" + Environment.NewLine;
            //-------------------------------------------------------------------------------------------------// 
            StudentName.Text = "";
            StudentAge.Text = "";
            StudentCredits.Text = "";
        }
        //Creating a txt file and saving graduated students in it
        private void SaveListButton_Click(object sender, RoutedEventArgs e)
        {
            SaveGraduatedStudentsToFile("graduated_students.txt");

            CustomMessageBox.Show("List of graduated students with at least 30 credits saved successfully.");
            StudentListTextBox.Text = "";
            
        }

        //Function for saving graduated file
        private void SaveGraduatedStudentsToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var student in GraduatedStudents)
                {
                    writer.WriteLine($"Name: {student.Name}, Age: {student.Age}, Credits: {student.Credits}");
                }
            }
        }
    }
  
}

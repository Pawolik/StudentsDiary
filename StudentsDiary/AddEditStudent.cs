using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {
        private int _studentId;
        private Student _student;

        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.FilePath);

        public AddEditStudent(int id = 0)
        {
            InitializeComponent();
            _studentId = id;

            GetStudentData();
            tbName.Select();
        }

        private void GetStudentData()
        {
            if (_studentId != 0)
            {
                Text = "Edytowanie danych ucznia";

                var students = _fileHelper.DeserializeFromFile();
                _student = students.FirstOrDefault(x => x.ID == _studentId);

                if (_student == null)
                {
                    throw new Exception("Brak użytkownika o podanym ID");
                }
                FillTextBoxes();
            }
        }

        private void FillTextBoxes()
        {
            tbID.Text = _student.ID.ToString();
            tbName.Text = _student.Name.ToString();
            tbLastName.Text = _student.LastName.ToString();
            tbMath.Text = _student.Math.ToString();
            tbPhysics.Text = _student.Physics.ToString();
            tbPolish.Text = _student.Polish.ToString();
            tbTech.Text = _student.Technology.ToString();
            tbForeign.Text = _student.Foreign.ToString();
            rtbComments.Text = _student.Description.ToString();
            cBoxActivities.Text = _student.Activities.ToString();
        }


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            if (_studentId != 0)
            {
                students.RemoveAll(x => x.ID == _studentId);
            }
            else
                AssignIdToNewStudent(students);

            AddNewUserToList(students);

            _fileHelper.SerializeToFile(students);

            Close();
        }

        private void AddNewUserToList(List<Student> students)
        {
            var student = new Student
            {
                ID = _studentId,
                Name = tbName.Text,
                LastName = tbLastName.Text,
                Description = rtbComments.Text,
                Polish = tbPolish.Text,
                Foreign = tbForeign.Text,
                Math = tbMath.Text,
                Physics = tbPhysics.Text,
                Technology = tbTech.Text,
                Activities = cBoxActivities.Checked
            };
            students.Add(student);
        }

        private void AssignIdToNewStudent(List<Student> students)
        {
            var studentWithHighestId = students.OrderByDescending(x => x.ID).FirstOrDefault();

            _studentId = studentWithHighestId == null ? 1 : studentWithHighestId.ID + 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cBoxActivities_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}

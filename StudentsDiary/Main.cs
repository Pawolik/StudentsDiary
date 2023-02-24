﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace StudentsDiary
{
    public partial class Main : Form
    {
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.FilePath);


        public Main()
        {
            InitializeComponent();
            RefreshDiary();
            SetColumnsHeader();

            var list = new List<int> { 2, 432, 22, 5, 85};
            var list2 = (from x in list
                        where x > 10
                        select x).ToList();
        }

        private void RefreshDiary()
        {
            var students = _fileHelper.DeserializeFromFile();
            dgvDiary.DataSource = students;
        }

        private void SetColumnsHeader()
        {
                dgvDiary.Columns[0].HeaderText = "Numer";
                dgvDiary.Columns[1].HeaderText = "Imie";
                dgvDiary.Columns[2].HeaderText = "Nazwisko";
                dgvDiary.Columns[3].HeaderText = "Uwagi";
                dgvDiary.Columns[4].HeaderText = "Matematyka";
                dgvDiary.Columns[5].HeaderText = "Technologia";
                dgvDiary.Columns[6].HeaderText = "Fizyka";
                dgvDiary.Columns[7].HeaderText = "Język polski";
                dgvDiary.Columns[8].HeaderText = "Język obcy";
        }

       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEdditStudent = new AddEditStudent();
            addEdditStudent.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznacz ucznia, którego dane chcesz edytowć");
                return;
            }
            var addEdditStudent = new AddEditStudent(Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value));
            addEdditStudent.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznacz ucznia, którego chcesz usunąć");
                return;
            }

            var selectedStudent = dgvDiary.SelectedRows[0];

            var confirmDelete = MessageBox.Show($"Czy na pewno chcesz usunąć ucznia {(selectedStudent.Cells[1].Value.ToString() + " " + selectedStudent.Cells[2].Value.ToString()).Trim()}",
                "Usuwanie ucznia", MessageBoxButtons.OKCancel);

            if (confirmDelete == DialogResult.OK)
            {
                DeleteStudent(Convert.ToInt32(selectedStudent.Cells[0].Value));
                RefreshDiary();
            }
        }

        private void DeleteStudent(int id)
        {
            var students = _fileHelper.DeserializeFromFile();
            students.RemoveAll(x => x.ID == id);
            _fileHelper.SerializeToFile(students);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void dgvDiary_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
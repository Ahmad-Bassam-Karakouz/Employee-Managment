using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

     
        // =========================
        // ADD EMPLOYEE FIELDS
        // =========================

        private void AddEmployeeName(ListViewItem item)
        {
            item.SubItems.Add(nameTextBox.Text);
        }

        private void AddEmployeeGender(ListViewItem item)
        {
            if (maleRadioButton.Checked)
                item.SubItems.Add("Male");
            else if (femaleRadioButton.Checked)
                item.SubItems.Add("Female");
            else
                item.SubItems.Add("Not Specified");
        }

        private void AddEmployeeDepartment(ListViewItem item)
        {
            item.SubItems.Add(departmentComboBox.Text);
        }

        private void AddEmployeeSkills(ListViewItem item)
        {
            string skills = "";
            foreach (var skill in skillsCheckedListBox.CheckedItems)
                skills += skill.ToString() + ",";

            if (skills.Length > 0)
                skills = skills.Remove(skills.Length - 1, 1);
            else
                skills = "No Skills Selected";

            item.SubItems.Add(skills);
        }

        private void AddEmployeeBirthDate(ListViewItem item)
        {
            string birthDate = birthDatePicker.Value.ToString("dd - MMM - yyyy");
            item.SubItems.Add(birthDate);
        }

        // =========================
        // CLEAR FORM
        // =========================

        private void ClearForm()
        {
            formErrorProvider.Clear();

            idTextBox.Clear();
            nameTextBox.Clear();

            maleRadioButton.Checked = false;
            femaleRadioButton.Checked = false;

            departmentComboBox.SelectedIndex = -1;

            foreach (int i in skillsCheckedListBox.CheckedIndices)
                skillsCheckedListBox.SetItemChecked(i, false);

            skillsCheckedListBox.ClearSelected();

            birthDatePicker.Value = DateTime.Now;
            birthDatePicker.Checked = false;
        }

        // =========================
        // VALIDATION
        // =========================

        private bool ValidateEmployee()
        {
            if (string.IsNullOrWhiteSpace(idTextBox.Text)) return false;
            if (string.IsNullOrWhiteSpace(nameTextBox.Text)) return false;
            if (string.IsNullOrWhiteSpace(departmentComboBox.Text)) return false;
            if (!maleRadioButton.Checked && !femaleRadioButton.Checked) return false;

            if (birthDatePicker.Value > DateTime.Now)
                return false;

            int age = DateTime.Now.Year - birthDatePicker.Value.Year;
            if (birthDatePicker.Value.Date > DateTime.Now.AddYears(-age)) age--;

            if (age < 18 || age > 60) return false;

            return true;
        }

        // =========================
        // ADD EMPLOYEE
        // =========================

        private void AddEmployee()
        {
            if (!ValidateEmployee())
            {
                MessageBox.Show("Invalid Employee Data");
                return;
            }

            ListViewItem item = new ListViewItem(idTextBox.Text);

            AddEmployeeName(item);
            AddEmployeeGender(item);
            AddEmployeeDepartment(item);
            AddEmployeeSkills(item);
            AddEmployeeBirthDate(item);

            employeeListView.Items.Add(item);

            ClearForm();
        }

        // =========================
        // EDIT EMPLOYEE FIELDS
        // =========================

        private void EditEmployeeId(ListViewItem item)
        {
            idTextBox.Text = item.SubItems[0].Text;
        }

        private void EditEmployeeName(ListViewItem item)
        {
            nameTextBox.Text = item.SubItems[1].Text;
        }

        private void EditEmployeeGender(ListViewItem item)
        {
            string gender = item.SubItems[2].Text;
            if (gender == "Male") maleRadioButton.Checked = true;
            else if (gender == "Female") femaleRadioButton.Checked = true;
        }

        private void EditEmployeeDepartment(ListViewItem item)
        {
            departmentComboBox.Text = item.SubItems[3].Text;
        }

        private void EditEmployeeSkills(ListViewItem item)
        {
            string skills = item.SubItems[4].Text;
            if (skills == "No Skills Selected") return;

            string[] skillArray = skills.Split(',');
            for (int i = 0; i < skillsCheckedListBox.Items.Count; i++)
                skillsCheckedListBox.SetItemChecked(i, false);

            foreach (string skill in skillArray)
            {
                for (int i = 0; i < skillsCheckedListBox.Items.Count; i++)
                {
                    if (skillsCheckedListBox.Items[i].ToString() == skill)
                    {
                        skillsCheckedListBox.SetItemChecked(i, true);
                        break;
                    }
                }
            }
        }

        private void EditEmployeeBirthDate(ListViewItem item)
        {
            string birthDateText = item.SubItems[5].Text;
            if (DateTime.TryParse(birthDateText, out DateTime birthDate))
                birthDatePicker.Value = birthDate;
        }

        // =========================
        // EDIT EMPLOYEE
        // =========================

        private void EditEmployee()
        {
            if (employeeListView.SelectedItems.Count == 0) return;

            ListViewItem item = employeeListView.SelectedItems[0];

            EditEmployeeId(item);
            EditEmployeeName(item);
            EditEmployeeGender(item);
            EditEmployeeDepartment(item);
            EditEmployeeSkills(item);
            EditEmployeeBirthDate(item);

           
        }


        // =========================
        // UPDATE EMPLOYEE FIELDS
        // =========================

        private void UpdateEmployeeId(ListViewItem item)
        {
            item.SubItems[0].Text = idTextBox.Text;
        }

        private void UpdateEmployeeName(ListViewItem item)
        {
            item.SubItems[1].Text = nameTextBox.Text;
        }

        private void UpdateEmployeeGender(ListViewItem item)
        {
            if (maleRadioButton.Checked)
            {
                item.SubItems[2].Text = "Male";
            }
            else if (femaleRadioButton.Checked)
            {
                item.SubItems[2].Text = "Female";
            }
            else
            {
                item.SubItems[2].Text = "Not Specified";
            }
        }

        private void UpdateEmployeeDepartment(ListViewItem item)
        {
            item.SubItems[3].Text = departmentComboBox.Text;
        }

        private void UpdateEmployeeSkills(ListViewItem item)
        {
            string skills = "";
            foreach (var skill in skillsCheckedListBox.CheckedItems)
                skills += skill.ToString() + ",";

            if (skills.Length > 0)
                skills = skills.Remove(skills.Length - 1, 1);
            else
                skills = "No Skills Selected";
            item.SubItems[4].Text = skills;
        }

        private void UpdateEmployeeBirthDate(ListViewItem item)
        {

            item.SubItems[5].Text = birthDatePicker.Value.ToString("dd/MM/yyyy");
        }

        private void UpdateEmployee()
        {
            if (employeeListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an employee to update.");
                return;
            }


            ListViewItem item = employeeListView.SelectedItems[0];

            UpdateEmployeeId(item);
            UpdateEmployeeName(item);
            UpdateEmployeeGender(item);
            UpdateEmployeeDepartment(item);
            UpdateEmployeeSkills(item);
            UpdateEmployeeBirthDate(item);
                     

            ClearForm();

            employeeListView.SelectedItems.Clear();

            bt_add.Enabled = true;

            MessageBox.Show("Employee updated successfully");

        }


        // =========================
        // DELETE EMPLOYEE FIELDS
        // =========================

        private void DeleteEmployee()
        {
            if(!(employeeListView.SelectedItems.Count>0))
            {
                MessageBox.Show("There are no specific elements !!");
                return;
            }

          if(MessageBox.Show("Are You Sure For Delete This Employee ? ","",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
          {
                employeeListView.SelectedItems[0].Remove();

                MessageBox.Show("Deleted Successfully");
          }

          
        }

        // =========================
        // VIEW CHANGER
        // =========================

        private void ChangeView(View view)
        {
            employeeListView.View = view;
        }


        // =========================
        // SEARCH EMPLOYEE
        // =========================

        private void SearchEmployeeById()
        {
            if(SearchTextBox.Text=="")
            {
                return;
            }

            employeeListView.SelectedItems.Clear();

            bool found=false;

            foreach (ListViewItem item in employeeListView.Items)
            {
                if(item.Text==SearchTextBox.Text)
                {
                    item.Selected = true;
                    item.EnsureVisible();
                    found = true;
                    break;
                }
            }

            if(found)
            {
                MessageBox.Show("Employee is found");
            }

            else
            {
                MessageBox.Show("Employee is not found");
            }

        }

        private void SearchEmployeeByPartialMatch()
        {
            employeeListView.SelectedItems.Clear();

            string Search = SearchTextBox.Text.Trim();

            if (string.IsNullOrEmpty(Search)) return;


            bool found = false;

            foreach(ListViewItem item in employeeListView.Items)
            {
                if(item.SubItems[0].Text.IndexOf(Search,StringComparison.OrdinalIgnoreCase)>=0  ||
                   item.SubItems[1].Text.IndexOf(Search, StringComparison.OrdinalIgnoreCase)>=0)
                {
                    item.Selected = true;
                    item.EnsureVisible();
                    found = true;                  
                }
            }

            SearchTextBox.Clear();

            if(!found)
            {
                MessageBox.Show("No employee found matching the search.");
            }


        }

        // =========================
        // EVENTS
        // =========================

        private void addButton_Click(object sender, EventArgs e)
        {
            AddEmployee();
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeView(View.Details);
        }

        private void smallIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeView(View.SmallIcon);
        }

        private void largeIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeView(View.LargeIcon);
        }

        private void tileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeView(View.Tile);
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeView(View.List);
        }
           
        private void Form1_Load(object sender, EventArgs e)
        {


        }
        
        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bt_add.Enabled = false;
            bt_update.Visible = true;
            EditEmployee();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteEmployee();
        }

        private void bt_update_Click(object sender, EventArgs e)
        {
            bt_update.Visible = false;
            UpdateEmployee();           
        }

        private void bt_search_Click(object sender, EventArgs e)
        {

            SearchEmployeeByPartialMatch();
           // SearchEmployeeById();
        }
    }
}

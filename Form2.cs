using EmploymentBureau.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmploymentBureau
{
    public partial class Form2 : Form
    {
        public Form2(Employee employee)
        {
            InitializeComponent();
            Employee _employee = employee;
            PopulateControls(_employee);
        }

        private void PopulateControls(Employee _employee)
        {
            if (_employee != null)
            {
                textBox1.Text = _employee.Name;
                textBox2.Text = _employee.Age.ToString();
                textBox3.Text = _employee.Male ? "Мужской" : "Женский";
                if (_employee.Passport != null)
                {
                    textBox4.Text = _employee.Passport.Number.ToString();
                    textBox5.Text = _employee.Passport.IssueDate.ToShortDateString();
                    textBox6.Text = _employee.Passport.EndDate.ToShortDateString();
                }
                else
                {
                    textBox4.Text = "Нет данных";
                    textBox5.Text = "Нет данных";
                    textBox6.Text = "Нет данных";
                }


                if (_employee.WorkExperience != null && _employee.WorkExperience.Length > 0)
                {
                    listBox1.Items.Clear();
                    foreach (string position in _employee.WorkExperience)
                    {
                        listBox1.Items.Add(position);
                    }
                }
                else
                {
                    listBox1.Items.Add("Нет данных");
                }


                if (_employee.Passport != null && _employee.Passport.Registrations != null && _employee.Passport.Registrations.Length > 0)
                {
                    listBox2.Items.Clear();
                    foreach (string registration in _employee.Passport.Registrations)
                    {
                        listBox2.Items.Add(registration);
                    }
                }
                else
                {
                    listBox2.Items.Add("Нет данных");
                }


                if (_employee.Passport != null && _employee.Passport.Marriages != null && _employee.Passport.Marriages.Length > 0)
                {
                    listBox3.Items.Clear();
                    foreach (string marriage in _employee.Passport.Marriages)
                    {
                        listBox3.Items.Add(marriage);
                    }
                }
                else
                {
                    listBox3.Items.Add("Нет данных");
                }
            }
        }
    }
}

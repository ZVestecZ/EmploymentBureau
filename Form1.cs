using EmploymentBureau.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace EmploymentBureau
{
    public partial class EmploymentBureau : Form
    {
        private BindingList<Employee> employeeBindingList = new BindingList<Employee>();
        private WorkplaceManager workplaceManager = new WorkplaceManager();
        public EmploymentBureau()
        {
            InitializeComponent();
            EmployeeList employeeList = new EmployeeList();
            //Таблица
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = employeeBindingList;
            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "Name";
            nameColumn.HeaderText = "ФИО";
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            nameColumn.Resizable = DataGridViewTriState.False;
            dataGridView1.Columns.Add(nameColumn);

            PopulateTreeView();
        }

        private EmployeeList DeserializeEmployeeList(string filePath)
        {
            EmployeeList employeeList = null;
            XmlSerializer serializer = new XmlSerializer(typeof(EmployeeList));

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                try
                {
                    employeeList = (EmployeeList)serializer.Deserialize(fileStream);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка десериализации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            return employeeList;
        }
        private void SerializeEmployeeList(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(EmployeeList));

            EmployeeList employeeListToSave = new EmployeeList { Employees = new List<Employee>(employeeBindingList) };

            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, employeeListToSave);
            }
        }


        private void PopulateTreeView()
        {
            treeViewWorkplaces.Nodes.Clear();

            TreeNode rootNode = new TreeNode("Места работы");
            treeViewWorkplaces.Nodes.Add(rootNode);

            List<Workplace> workplaces = workplaceManager.GetWorkplaces();

            foreach (Workplace workplace in workplaces)
            {
                TreeNode companyNode = new TreeNode(workplace.Title);
                companyNode.Tag = workplace;

                TreeNode postNode = new TreeNode($"Должность: {workplace.Post}, Зарплата: {workplace.Wages}");
                companyNode.Nodes.Add(postNode);

                TreeNode documentsNode = new TreeNode("Документы");
                foreach (string document in workplace.Documents)
                {
                    documentsNode.Nodes.Add(document);
                }
                companyNode.Nodes.Add(documentsNode);
                documentsNode.Collapse();

                TreeNode schedulesNode = new TreeNode("Графики работы");
                foreach (string schedule in workplace.WorkSchedules)
                {
                    schedulesNode.Nodes.Add(schedule);
                }
                companyNode.Nodes.Add(schedulesNode);
                schedulesNode.Collapse();

                rootNode.Nodes.Add(companyNode);
            }
            rootNode.Expand();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.Title = "Выберите XML-файл с данными о сотрудниках";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    EmployeeList employeeList = DeserializeEmployeeList(filePath);

                    if (employeeList != null && employeeList.Employees != null)
                    {
                        foreach (Employee employee in employeeList.Employees)
                        {
                            BindingList<Employee> bindingList = dataGridView1.DataSource as BindingList<Employee>;
                            if (bindingList != null)
                            {
                                bindingList.Add(employee);
                                dataGridView1.Refresh();
                            }
                        }
                        MessageBox.Show("Данные успешно загружены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Файл не содержит данных о сотрудниках или имеет неверный формат.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при десериализации файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = "output.xml";

            try
            {
                SerializeEmployeeList(filePath);
                MessageBox.Show("Данные успешно сохранены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сериализации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Employee selectedEmployee = (Employee)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                if (selectedEmployee != null)
                {
                    Form2 form2 = new Form2(selectedEmployee);

                    form2.ShowDialog();
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.Title = "Выберите JSON-файл с данными о местах работы";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath2 = openFileDialog.FileName;

                try
                {
                    workplaceManager.DeserializeFromJson(filePath2);
                    PopulateTreeView();
                    MessageBox.Show("Данные успешно загружены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output2.json");

            try
            {
                workplaceManager.SerializeToJson(filePath2);
                MessageBox.Show($"Данные успешно сохранены в: {filePath2}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Главное окно разделено на 2 части \n" +
                " слева сущности людей и паспортов из xml файла (для примера Employees.xml в папке с sln) \n" +
                " справа сущность места работы из json файла (для примера list_works.json там же) \n" +
                " двойное нажатие в таблице откроет окно подробной информации \n" +
                " файлы сохроняются в bin>Debug \n" +
                "p.s. только что (20:13) форма 1 не загрузилась, выдала ошибку, и все элементы пропали, мне очень грустно(((", "", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}

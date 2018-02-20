using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Purii_Lab4
{
    public class Form1 : System.Windows.Forms.Form
    {
        private Panel buttonPanel = new Panel();
        private DataGridView studentsDataGridView = new DataGridView();
        private Button addNewRowButton = new Button();
        private Button deleteRowButton = new Button();
        private Button exportRowsButton = new Button();
        private Button importRowsButton = new Button();

        public Form1()
        {
            this.Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(System.Object sender, System.EventArgs e)
        {
            SetupLayout();
            SetupDataGridView();
            PopulateDataGridView();
        }

        private void studentsDataGridView_CellFormatting(object sender,
            System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            if (e != null)
            {
                if (this.studentsDataGridView.Columns[e.ColumnIndex].Name == "Release Date")
                {
                    if (e.Value != null)
                    {
                        try
                        {
                            e.Value = DateTime.Parse(e.Value.ToString())
                                .ToLongDateString();
                            e.FormattingApplied = true;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("{0} is not a valid date.", e.Value.ToString());
                        }
                    }
                }
            }
        }

        private void addNewRowButton_Click(object sender, EventArgs e)
        {
            this.studentsDataGridView.Rows.Add();
        }

        private void deleteRowButton_Click(object sender, EventArgs e)
        {
            if (this.studentsDataGridView.SelectedRows.Count > 0 &&
                this.studentsDataGridView.SelectedRows[0].Index !=
                this.studentsDataGridView.Rows.Count - 1)
            {
                this.studentsDataGridView.Rows.RemoveAt(
                    this.studentsDataGridView.SelectedRows[0].Index);
            }
        }

        private void exportRowsButton_Click(object sender, EventArgs e)
        {
            Stream fileStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((fileStream = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter fileWriter = new StreamWriter(fileStream);

                    string columnHeaderText = "";

                    //Collecting DataGridView Column Header Text
                    int countColumn = studentsDataGridView.ColumnCount - 1;

                    if (countColumn >= 0)
                    {
                        columnHeaderText = studentsDataGridView.Columns[0].HeaderText;
                    }

                    for (int i = 1; i <= countColumn; i++)
                    {
                        columnHeaderText = columnHeaderText + ',' + studentsDataGridView.Columns[i].HeaderText;
                    }

                    //Writing Column Header Text in File
                    fileWriter.WriteLine(columnHeaderText);

                    //Collecting Data Rows
                    foreach (DataGridViewRow dataRowObject in studentsDataGridView.Rows)
                    {
                        //Checking for New Row in DataGridView 
                        if (!dataRowObject.IsNewRow)
                        {
                            string dataFromGrid = "";

                            dataFromGrid = dataRowObject.Cells[0].Value.ToString();

                            for (int i = 1; i <= countColumn; i++)
                            {
                                if (dataRowObject.Cells[i].Value != null)
                                {
                                    dataFromGrid += ',' + dataRowObject.Cells[i].Value.ToString();
                                }
                                else
                                {
                                    dataFromGrid += ',';
                                }
                            }

                            //Writing Data Rows in File         
                            fileWriter.WriteLine(dataFromGrid);
                        }
                    }

                    fileWriter.Flush();
                    fileWriter.Close();
                    fileStream.Close();
                }
            }
        }

        private void importRowsButton_Click(object sender, EventArgs e)
        {
            Stream fileStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((fileStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (fileStream)
                        {
                            StreamReader fileReader = new StreamReader(fileStream);

                            if (fileReader.Peek() != -1)
                            {
                                string fileRow = fileReader.ReadLine();
                                string[] fileDataField = fileRow.Split(',');

                                studentsDataGridView.Rows.Clear();
                                studentsDataGridView.Columns.Clear();

                                //Adding Column Header to DataGridView
                                for (int i = 0; i < fileDataField.Length; i++)
                                {
                                    DataGridViewTextBoxColumn columnDataGridTextBox = new DataGridViewTextBoxColumn();
                                    columnDataGridTextBox.Name = fileDataField[i];
                                    columnDataGridTextBox.HeaderText = fileDataField[i];
                                    studentsDataGridView.Columns.Add(columnDataGridTextBox);
                                }

                                //Adding Data to DataGridView
                                while (fileReader.Peek() != -1)
                                {
                                    fileRow = fileReader.ReadLine();
                                    fileDataField = fileRow.Split(',');
                                    studentsDataGridView.Rows.Add(fileDataField);
                                }
                            }

                            fileReader.Close();
                            fileStream.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void SetupLayout()
        {
            this.Size = new Size(600, 500);

            addNewRowButton.Text = "Add Row";
            addNewRowButton.Location = new Point(10, 10);
            addNewRowButton.Click += new EventHandler(addNewRowButton_Click);

            deleteRowButton.Text = "Delete Row";
            deleteRowButton.Location = new Point(100, 10);
            deleteRowButton.Click += new EventHandler(deleteRowButton_Click);

            exportRowsButton.Text = "Export";
            exportRowsButton.Location = new Point(190, 10);
            exportRowsButton.Click += new EventHandler(exportRowsButton_Click);

            importRowsButton.Text = "Import";
            importRowsButton.Location = new Point(280, 10);
            importRowsButton.Click += new EventHandler(importRowsButton_Click);

            buttonPanel.Controls.Add(addNewRowButton);
            buttonPanel.Controls.Add(deleteRowButton);
            buttonPanel.Controls.Add(exportRowsButton);
            buttonPanel.Controls.Add(importRowsButton);
            buttonPanel.Height = 50;
            buttonPanel.Dock = DockStyle.Bottom;

            this.Controls.Add(this.buttonPanel);
        }

        private void SetupDataGridView()
        {
            this.Controls.Add(studentsDataGridView);

            studentsDataGridView.ColumnCount = 5;

            studentsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            studentsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            studentsDataGridView.ColumnHeadersDefaultCellStyle.Font =
                new Font(studentsDataGridView.Font, FontStyle.Bold);

            studentsDataGridView.Name = "songsDataGridView";
            studentsDataGridView.Location = new Point(8, 8);
            studentsDataGridView.Size = new Size(500, 250);
            studentsDataGridView.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            studentsDataGridView.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            studentsDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            studentsDataGridView.GridColor = Color.Black;
            studentsDataGridView.RowHeadersVisible = false;

            studentsDataGridView.Columns[0].Name = "First name";
            studentsDataGridView.Columns[1].Name = "Second name";
            studentsDataGridView.Columns[2].Name = "Last name";
            studentsDataGridView.Columns[3].Name = "Date of birth";
            studentsDataGridView.Columns[4].Name = "Year of study";
            studentsDataGridView.Columns[4].DefaultCellStyle.Font =
                new Font(studentsDataGridView.DefaultCellStyle.Font, FontStyle.Italic);

            studentsDataGridView.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            studentsDataGridView.MultiSelect = false;
            studentsDataGridView.Dock = DockStyle.Fill;

            studentsDataGridView.CellFormatting += new
                DataGridViewCellFormattingEventHandler(
                studentsDataGridView_CellFormatting);
        }

        private void PopulateDataGridView()
        {

            string[] row0 = { "Ann", "Marry", "River",
            "11/11/1998", "4" };
            string[] row1 = { "Jane", "Carry", "Woolf",
            "15/03/1999", "3" };
            string[] row2 = { "Archie", "Augustin", "Danne",
            "16/09/1999", "3" };
            string[] row3 = { "Oviver", "Jack", "Johnson",
            "17/01/1998", "4" };
            string[] row4 = { "Georgia", "Ellie", "Cooper",
            "21/07/2000", "2" };
            string[] row5 = { "Matthew", "David", "Johnson",
            "14/12/2000", "2" };
            string[] row6 = { "Noah", "Leo", "Dress",
            "20/01/2000", "2" };

            studentsDataGridView.Rows.Add(row0);
            studentsDataGridView.Rows.Add(row1);
            studentsDataGridView.Rows.Add(row2);
            studentsDataGridView.Rows.Add(row3);
            studentsDataGridView.Rows.Add(row4);
            studentsDataGridView.Rows.Add(row5);
            studentsDataGridView.Rows.Add(row6);

            studentsDataGridView.Columns[0].DisplayIndex = 0;
            studentsDataGridView.Columns[1].DisplayIndex = 1;
            studentsDataGridView.Columns[2].DisplayIndex = 2;
            studentsDataGridView.Columns[3].DisplayIndex = 3;
            studentsDataGridView.Columns[4].DisplayIndex = 4;
        }
    }
}

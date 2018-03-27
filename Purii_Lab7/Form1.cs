using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;

/*
 IMPORTANT
 MS Access file is created automatically every time the application starts.
 Changes are saved to the current Access file in the debug directory.
 If you reload the application, you won't see any changes,
 because new file has been created.
 You can also specify your file path here (line 73):
 String connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\db2.accdb";
 */

public class Form1 : System.Windows.Forms.Form
{
    private DataGridView dataGridView1 = new DataGridView();
    private BindingSource bindingSource1 = new BindingSource();
    private OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
    private Button reloadButton = new Button();
    private Button submitButton = new Button();

    // Initialize the form.
    public Form1()
    {
        dataGridView1.Dock = DockStyle.Fill;

        reloadButton.Text = "RELOAD";
        submitButton.Text = "SAVE";
        reloadButton.Click += new System.EventHandler(reloadButton_Click);
        submitButton.Click += new System.EventHandler(submitButton_Click);

        FlowLayoutPanel panel = new FlowLayoutPanel();
        panel.Dock = DockStyle.Top;
        panel.AutoSize = true;
        panel.Controls.AddRange(new Control[] { reloadButton, submitButton });

        this.Controls.AddRange(new Control[] { dataGridView1, panel });
        this.Load += new System.EventHandler(Form1_Load);
        this.Text = "EMPLOYEES";

        this.Size = new System.Drawing.Size(450, 400);
    }

    private void Form1_Load(object sender, System.EventArgs e)
    {
        // Bind the DataGridView to the BindingSource
        // and load the data from the database.
        dataGridView1.DataSource = bindingSource1;
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        GetData("SELECT * FROM Employees");
    }

    private void reloadButton_Click(object sender, System.EventArgs e)
    {
        // Reload the data from the database.
        GetData(dataAdapter.SelectCommand.CommandText);
    }

    private void submitButton_Click(object sender, System.EventArgs e)
    {
        // Update the database with the user's changes.
        Console.WriteLine(
        dataAdapter.Update((DataTable)bindingSource1.DataSource));
    }

    private void GetData(string selectCommand)
    {
        try
        {
            String connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\db2.accdb";

            // Create a new data adapter based on the specified query.
            dataAdapter = new OleDbDataAdapter(selectCommand, connectionString);

            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(dataAdapter);
            commandBuilder.QuotePrefix = "[";
            commandBuilder.QuoteSuffix = "]";

            // Populate a new data table and bind it to the BindingSource.
            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            bindingSource1.DataSource = table;

            // Resize the DataGridView columns to fit the newly loaded content.
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString());
        }
    }
}
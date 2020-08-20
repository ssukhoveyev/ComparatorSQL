using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ComparatorSQL
{
    public partial class FormApp: Form
    {
        SqlConnection conn;

        public FormApp()
        {
            InitializeComponent();
            ConnectionSql();
            textBoxQuery1.Text = Properties.Settings.Default.lastQuery1;
            textBoxQuery2.Text = Properties.Settings.Default.lastQuery2;
        }

        private void подключениеКБазеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDBSettings f = new FormDBSettings();
            f.Show();
        }

        private void ConnectionSql()
        {
            buttonGO.Enabled = true;
            string dbUserName = Properties.Settings.Default.dbUsername;
            string dbUserPass = Properties.Settings.Default.dbPass;
            string dbSevName = Properties.Settings.Default.dbServer;
            string dbName = Properties.Settings.Default.dbName;
            conn = new SqlConnection($"uid={dbUserName};pwd={dbUserPass};MultipleActiveResultSets=True;database={dbName};server={dbSevName};Connection Timeout = 10;");

            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                buttonGO.Enabled = false;
                MessageBox.Show("Не удалось подключиться к базе данных." + Environment.NewLine + "Проверьте настройки");
            }
            finally
            {
                conn.Close();
            }
        }

        private void buttonGO_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.lastQuery1 = textBoxQuery1.Text;
            Properties.Settings.Default.lastQuery2 = textBoxQuery2.Text;
            Properties.Settings.Default.Save();

            bool isCorrect1 = LoadDataTable(textBoxQuery1.Text, dataGridView1);
            bool isCorrect2 = LoadDataTable(textBoxQuery2.Text, dataGridView2);

            if (isCorrect1 && isCorrect2)
            {
                Compare(dataGridView1, dataGridView2);
                Compare(dataGridView2, dataGridView1);
            }
            else
            {
                MessageBox.Show("Ошибка в запросе.");
            }
        }

        private void подключениеКБазеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConnectionSql();
        }

        private bool LoadDataTable(string query, DataGridView DataGridViewTarget)
        {
            bool isCorrect = true;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataGridViewTarget.DataSource = dt;
            }
            catch (Exception)
            {
                isCorrect = false;
            }

            return isCorrect;
            
        }

        private void Compare(DataGridView DataGridViewSourse, DataGridView DataGridViewCompared)
        {
            for (int a = 0; a < DataGridViewSourse.ColumnCount; a++)
            {
                for (int r = 0; r < DataGridViewSourse.RowCount; r++)
                {
                    if (dataGridView2.ColumnCount > a && dataGridView2.RowCount > r)
                    {
                        string cell1 = DataGridViewSourse[a, r].Value == null ? "" : DataGridViewSourse[a, r].Value.ToString();
                        string cell2 = DataGridViewCompared[a, r].Value == null ? "" : DataGridViewCompared[a, r].Value.ToString();

                        if (cell1 != cell2)
                            DataGridViewSourse[a, r].Style.BackColor = System.Drawing.Color.Gray;
                    }
                    else
                    {
                        DataGridViewSourse[a, r].Style.BackColor = System.Drawing.Color.Gray;
                    }
                }
            }
        }
        
    }
}

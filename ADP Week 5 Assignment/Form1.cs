using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ADP_Week_5_Assignment
{
    public partial class Form1 : Form
    {
        static string dbName = "Employees.db";
        static string tableName = "Employees";
        SQLiteConnection conn;
        public Form1()
        {
            InitializeComponent();
            try
            {
                conn = new SQLiteConnection($"Data Source={dbName}; Version = 3; New = True; Compress = True; ");
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand($"SELECT name FROM sqlite_master WHERE type='table';", conn);
                SQLiteDataReader dr = cmd.ExecuteReader();
                //checks and sees if rows exist, if they don't exists then it creates the table and populates it with the required data.
                if (!dr.HasRows)
                {
                    //dr must be closed before command text can be changed
                    dr.Close();
                    cmd.CommandText = $"create table {tableName}(EmpID integer primary key, EmpName nvarchar(50), EmpGender nvarchar(1), EmpHiringDate date)";
                    dr = cmd.ExecuteReader();
                    dr.Close();
                    cmd.CommandText = $"insert into {tableName} values(1, 'Employee 1', 'F', '2020-01-01'), (2, 'Employee 2', 'F', '2020-02-01'), (3, 'Employee 3', 'M', '2020-03-01'), (4, 'Employee 4', 'F', '2020-04-01')";
                    dr = cmd.ExecuteReader();
                    dr.Close();
                }
                //update the table
                btnRead_Click(null, null);
                conn.Close();
                
                
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SQLiteConnection($"Data Source={dbName}; Version = 3; New = True; Compress = True; ");
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand($"SELECT * from {tableName};", conn);
                SQLiteDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SQLiteConnection($"Data Source={dbName}; Version = 3; New = True; Compress = True; ");
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand($"insert into {tableName} values({txtBEmpID.Text}, '{txtBEmpName.Text}', '{txtBEmpGender.Text}', '{txtBEmpHireDate.Text}');", conn);
                SQLiteDataReader dr = cmd.ExecuteReader();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SQLiteConnection($"Data Source={dbName}; Version = 3; New = True; Compress = True; ");
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand($"update {tableName} set EmpName = '{txtBEmpName.Text}', EmpGender = '{txtBEmpGender.Text}', EmpHiringDate = '{txtBEmpHireDate.Text}' where EmpID = {txtBEmpID.Text};", conn);
                SQLiteDataReader dr = cmd.ExecuteReader();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SQLiteConnection($"Data Source={dbName}; Version = 3; New = True; Compress = True; ");
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand($"delete from {tableName} where EmpID = {txtBEmpID.Text};", conn);
                SQLiteDataReader dr = cmd.ExecuteReader();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

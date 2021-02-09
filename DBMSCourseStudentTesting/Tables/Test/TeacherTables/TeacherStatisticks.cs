using DBMSCourseStudentTesting.CLasses;
using DBMSCourseStudentTesting.Entityies;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DBMSCourseStudentTesting
{
    public partial class TeacherStatisticks : Form
    {
        private Test test;
        private User user;

        public Microsoft.Office.Interop.Excel.Application oXL { get; private set; }

        public TeacherStatisticks(Test test, User user)
        {
            InitializeComponent();

            this.test = test;
            this.user = user;
            Text = $"Статистика теста \"{test.Name.Trim()}\" ";
        }

        private void Statisticks_Load(object sender, EventArgs e)
        {
            using (StudentTestingEntities1 db = new StudentTestingEntities1())
            {


                string query = @"Select ag.Name as AcademicGroup,s.Surname,s.Name,s.Patronymic,e.ExamDate,e.Mark,
                                CONVERT(bit,(CASE WHEN e.Mark>=t.PassMark Then 1 ElSE 0 END)) As IsPassed 
                                from Examination as e
                                JOIN Student as s on e.idStudent = s.id
                                JOIN Test as t on t.id = e.idTest
								JOIN AcademicGroup as ag on s.idAcademicGroup = ag.id
                                Where t.id = @idTest";
                SqlParameter parameter = new SqlParameter("@idTest", test.id);
                var z = db.Database.SqlQuery<TestStatisticks>(query, parameter).ToList();

                dataGridView1.DataSource = z.ToList();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["AcademicGroup"].HeaderText = "Группа";
                dataGridView1.Columns["Surname"].HeaderText = "Фамилия";
                dataGridView1.Columns["Name"].HeaderText = "Имя";
                dataGridView1.Columns["Patronymic"].HeaderText = "Отчество";
                dataGridView1.Columns["Mark"].HeaderText = "Балл";
                dataGridView1.Columns["ExamDate"].HeaderText = "Дата сдачи теста";
                dataGridView1.Columns["isPassed"].HeaderText = "Сдано";

            }
        }

        private void изменитьТестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTest editTest = new EditTest(test, user);
            editTest.Show();
        }

        private void изменитьДоступКТестуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTestAccess editTestAccess = new EditTestAccess(test);
            editTestAccess.Show();
        }

        private void печатьОтсчётаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SFD.Filter = "Файлы Excel (*.xls; *.xlsx) | *.xls; *.xlsx";
            OFD.Filter = "Файлы Excel (*.xls; *.xlsx) | *.xls; *.xlsx";
            
            Microsoft.Office.Interop.Excel.Application firsExcel = null;
            Microsoft.Office.Interop.Excel.Workbook fistBook = null;
            Microsoft.Office.Interop.Excel.Workbook secondBook = null;

            MessageBox.Show("Выберите сначала файл с шаблоном, а потом место для отсчёта отсчёта");

            if (OFD.ShowDialog() == DialogResult.OK & SFD.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    firsExcel = new Microsoft.Office.Interop.Excel.Application(); //открыть эксель
                    fistBook = firsExcel.Workbooks.Open(@"C:\Games\pattern.xlsx", ReadOnly: false); //открыть файл
                    Microsoft.Office.Interop.Excel.Worksheet fistSheets = (Microsoft.Office.Interop.Excel.Worksheet)fistBook.Sheets[1]; //получить 1 лист

                    secondBook = firsExcel.Workbooks.Add("");
                    Microsoft.Office.Interop.Excel.Worksheet secondSheets = secondBook.ActiveSheet;


                    var lastCell = fistSheets.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell);//1 ячейку
                                                                                                                               //-------------------------------------
                    int lastColumn = (int)lastCell.Column;//!сохраним непосредственно требующееся в дальнейшем
                    int lastRow = (int)lastCell.Row;
                    //-------------------------------------
                    string[,] list = new string[lastCell.Column, lastCell.Row]; // массив значений с листа равен по размеру листу
                    int iLastRow = fistSheets.Cells[fistSheets.Rows.Count, "A"].End[Microsoft.Office.Interop.Excel.XlDirection.xlUp].Row;  //последняя заполненная строка в столбце А
                    var arrData = (object[,])fistSheets.Range["A1:Z" + iLastRow].Value;

                    String snp = "<SNP>";
                    String mark = "<Mark>";
                    String examDate = "<ExamDate>";
                    String isPassed = "<isPassed>";
                    String ag = "<AcademicGroup>";

                    List<string> keys = new List<string>();
                    List<MyCell> cells = new List<MyCell>();

                    var p = arrData.GetLength(0);
                    var f = arrData.GetLength(1);

                    for (int i = 1; i <= arrData.GetLength(0); i++)
                    {
                        for (int y = 1; y <= arrData.GetLength(1); y++)
                        {
                            if (y == 26)
                            {
                                var u = 5;
                            }

                            if (arrData[i, y] == null)
                                continue;
                            var text = arrData[i, y].ToString();
                            CompareText(snp, keys, cells, i, y, text);
                            CompareText(mark, keys, cells, i, y, text);
                            CompareText(examDate, keys, cells, i, y, text);
                            CompareText(isPassed, keys, cells, i, y, text);
                            CompareText(ag, keys, cells, i, y, text);
                        }
                    }
                    var ttext = keys[0];
                    string addingQuery = "";
                    if (ttext == snp)
                    {
                        addingQuery = " order by Surname";
                    }
                    else
                    {
                        ttext = ttext.Replace("<", "");
                        ttext = ttext.Replace(">", "");
                        addingQuery = $" order by {ttext}";
                    }


                    using (StudentTestingEntities1 db = new StudentTestingEntities1())
                    {
                        string query = @"Select ag.Name as AcademicGroup,s.Surname,s.Name,s.Patronymic,e.ExamDate,e.Mark,
                                CONVERT(bit,(CASE WHEN e.Mark>=t.PassMark Then 1 ElSE 0 END)) As IsPassed 
                                from Examination as e
                                JOIN Student as s on e.idStudent = s.id
                                JOIN Test as t on t.id = e.idTest
								JOIN AcademicGroup as ag on s.idAcademicGroup = ag.id
                                Where t.id = @idTest";

                        query += addingQuery;
                        SqlParameter parameter = new SqlParameter("@idTest", test.id);
                        var z = db.Database.SqlQuery<TestStatisticks>(query, parameter).ToList();


                        secondSheets.Cells[1, 1] = $"Результат тестирования теста {test.Name}";
                        for (int i = 0; i < z.Count; i++)
                        {
                            int index = keys.IndexOf(snp);
                            if (index != -1)
                            {
                                string s = $"{z[i].Surname.Trim()} {z[i].Name.Trim()} {z[i].Patronymic.Trim()}";
                                secondSheets.Cells[i + 2, cells[index].Y ] = s;
                                secondSheets.Columns[cells[index].Y ].ColumnWidth = 30;
                            }
                            index = keys.IndexOf(mark);
                            if (index != -1)
                            {
                                secondSheets.Cells[i + 2, cells[index].Y ] = z[i].Mark;

                            }
                            index = keys.IndexOf(examDate);
                            if (index != -1)
                            {
                                secondSheets.Cells[i + 2, cells[index].Y ] = z[i].ExamDate;
                                secondSheets.Columns[cells[index].Y].ColumnWidth = 20;

                            }
                            index = keys.IndexOf(isPassed);
                            if (index != -1)
                            {
                                secondSheets.Cells[i + 2, cells[index].Y] = z[i].IsPassed;

                            }
                            index = keys.IndexOf(ag);
                            if (index != -1)
                            {
                                secondSheets.Cells[i + 2, cells[index].Y] = z[i].AcademicGroup;

                            }
                        }

                    }

                    

                    secondBook.SaveAs(SFD.FileName, Type.Missing, Type.Missing,
                                    Type.Missing, Type.Missing, Type.Missing,
                                    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing,
                                    Type.Missing, Type.Missing, Type.Missing);


                }
                catch (Exception j)
                {
                    MessageBox.Show(j.ToString());
                }
                finally
                {
                    if (fistBook != null)
                    {
                        fistBook.Close(false, Type.Missing, Type.Missing); //закрыть не сохраняя
                    }
                    if (secondBook != null)
                    {
                        secondBook.Close(false, Type.Missing, Type.Missing); // выйти из экселя
                    }
                    if (firsExcel != null)
                    {
                        firsExcel.Quit(); // выйти из экселя
                    }
                }
            }
            
        }

        private static void CompareText(string snp, List<string> keys, List<MyCell> cells, int i, int y, string text)
        {
            if (text == snp)
            {
                keys.Add(snp);
                cells.Add(new MyCell(i, y));
            }
        }

        class MyCell
        {
            public int X { private set; get; }
            public int Y { private set; get; }
            public MyCell(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        class TestStatisticks
        {
            public string AcademicGroup { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string Patronymic { get; set; }
            public DateTime ExamDate { get; set; }
            public short Mark { get; set; }
            public bool IsPassed { get; set; }
        }

    }
}





//                worksheet.Name = "Данные";
//                worksheet.Cells[1, 1] = "Данные";
//                //for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
//                //{
//                //    worksheet.Cells[2, i] = dataGridView1[i - 1, 0].Value;

//                //    //worksheet.Cells[2, i].Font.Color = Color.Red;
//                //}
//                for (int i = 0; i<dataGridView1.RowCount; i++)
//                    for (int j = 0; j<dataGridView1.ColumnCount; j++)
//                    {
//                        worksheet.Cells[i + 2, j + 1] = dataGridView1[j, i].Value;
//                    }
//                workbook.SaveAs(SFD.FileName, Type.Missing, Type.Missing,
//                Type.Missing, Type.Missing, Type.Missing,
//                Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing,
//                Type.Missing, Type.Missing, Type.Missing);
//                app.Quit();
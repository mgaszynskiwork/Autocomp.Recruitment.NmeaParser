using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace NmeaParser.ViewModels
{
    public class TableCreator
    {
        public TableCreator()
        { }

        public void CreateTable(Table table, string[] headers, List<List<string>> fieldsList)
        {
            ColorTable(table, headers.Length);
            CreateHeader(table, headers);
            EnterValue(table, fieldsList);
        }

        private void ColorTable(Table table, int numberOfColumns)
        {
            table.RowGroups.Clear();
            table.CellSpacing = 10;
            table.Background = Brushes.White;

            for (int x = 0; x < numberOfColumns; x++)
            {
                table.Columns.Add(new TableColumn());

                table.Columns[x].Background = x % 2 == 0 ? Brushes.Beige : Brushes.LightSteelBlue;
            }
        }

        private void CreateHeader(Table table, string[] headers)
        {
            table.RowGroups.Add(new TableRowGroup());
            table.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = table.RowGroups[0].Rows[0];
            currentRow.FontWeight = FontWeights.UltraBold;

            for (int i = 0; i < headers.Length; i++)
            {
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(headers[i]))));
            }
        }

        private void EnterValue(Table table, List<List<string>> fieldsList)
        {
            int line = 1;
            TableRow currentRow;
            foreach (var fields in fieldsList)
            {
                table.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table.RowGroups[0].Rows[line];
                foreach (var field in fields)
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(field))));
                }
                line++;
            }
        }
    }
}
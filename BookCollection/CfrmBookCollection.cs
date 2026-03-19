using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookCollection
{

    // Added enum and Book model used by the form
    public enum Genre
    {
        HistoricalFiction,
        NonFiction,
        Mystery,
        Romance,
        Fantasy,
        Thriller,
        Other
    }

    public class Book
    {
        public string ISBN { get; }
        public string Title { get; }
        public string Author { get; }
        public Genre Genre { get; }

        public Book(string isbn, string title, string author, Genre genre)
        {
            ISBN = isbn ?? string.Empty;
            Title = title ?? string.Empty;
            Author = author ?? string.Empty;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"{ISBN} - {Title} by {Author} ({Genre})";
        }
    }
    public partial class CfrmBookCollection : Form
    {
        private readonly List<Book> m_books = new List<Book>();

        public CfrmBookCollection()
        {
            InitializeComponent();
            cmbGenre.Items.Add("HistoricalFiction");
            cmbGenre.Items.Add("NonFiction");
            cmbGenre.Items.Add("Mystery");
            cmbGenre.Items.Add("Romance");
            cmbGenre.Items.Add("Fantasy");
            cmbGenre.Items.Add("Thriller");
            cmbGenre.Items.Add("Other");

            AddTestData();
        }


        private void AddTestData()
        {
            m_books.Add(new Book("9780143127741", "The Nightingale", "Kristin Hannah", Genre.HistoricalFiction));

            
            m_books.Add(new Book("9780062315007", "Sapiens: A Brief History of Humankind", "Yuval Noah Harari", Genre.NonFiction));

            
            m_books.Add(new Book("9780307277671", "The Girl with the Dragon Tattoo", "Stieg Larsson", Genre.Mystery));
        }

        private void btnViewCollection_Click(object sender, EventArgs e)
        {
            lstbxBooks.Items.Clear();

            foreach (Book b in m_books)
            {
                lstbxBooks.Items.Add(b.ToString());
            }

        }//btnViewCollection_Click

        private void btnAddBook_Click(object sender, EventArgs e)
        {

            string isbn = txtISBN.Text.Trim();
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string genreText = cmbGenre.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a title.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Enum.TryParse<Genre>(genreText, true, out Genre parsedGenre))
            {
                var book = new Book(isbn, title, author, parsedGenre);
                m_books.Add(book);

                MessageBox.Show("Book added to collection.", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Optionally clear fields after successful add
                txtISBN.Clear();
                txtTitle.Clear();
                txtAuthor.Clear();
                cmbGenre.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Invalid genre specified.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        
    }//class
}//namespace

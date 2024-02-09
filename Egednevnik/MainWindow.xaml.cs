using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Egednevnik
{
    public partial class MainWindow : Window
    {
        private List<Note> List_of_notes = Converter_to_JSON.MyDerealize<List<Note>>();
        public MainWindow()
        {
            InitializeComponent();
            Date_Changer.Text = DateTime.Now.Date.ToString();

            List_of_Notes_Show();
        }
        private void List_of_Notes_Show()
        {
            if (List_of_notes != null && List_of_notes.Count > 0)
            {
                string Date_in_window = Date_Changer.Text;

                var filteredNotes = List_of_notes.Where(note => note.Date_of_note == Date_in_window).Select(note => note.Name_of_note).ToList();

                List_of_Notes.ItemsSource = filteredNotes;
            }
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            Note note = new Note();

            note.Name_of_note = TextBox1.Text;
            note.Discription = TextBox2.Text;
            note.Date_of_note = Date_Changer.Text;

            if (TextBox1.Text == "")
            {
                MessageBox.Show("Вы не ввели название заметки!!!");
                MessageBox.Show("Попробуйте снова");
            }
            else if (List_of_notes.Exists(note => note.Name_of_note == TextBox1.Text && note.Date_of_note == Date_Changer.Text))
            { 
                MessageBox.Show($"Заметка с именем '{TextBox1.Text}' уже существует в данной дате.\nПопробуйте другое имя");
                TextBox1.Text = "";
            }
            else if (TextBox2.Text == "")
            {
                MessageBox.Show("Вы не ввели описание заметки!!!");
                MessageBox.Show("Попробуйте снова");
            }
            else
            {
                List_of_notes.Add(note);
                Converter_to_JSON.MySerealize(List_of_notes);
                List_of_Notes_Show();

                TextBox1.Text = "";
                TextBox2.Text = "";
            }
        }

        private void Date_Changer_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_of_notes != null && List_of_notes.Count > 0)
            {
                string Date_in_window = Date_Changer.Text;

                // Фильтруем заметки по дате
                var filteredNotes = List_of_notes.Where(note => note.Date_of_note == Date_in_window).Select(note => note.Name_of_note).ToList();

                List_of_Notes.ItemsSource = filteredNotes;
            }
        }

        private void List_of_Notes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_of_Notes.SelectedItem != null)
            {
                string selectedName = List_of_Notes.SelectedItem as string;
                if (selectedName != null)
                {
                    Note selectedNote = List_of_notes.FirstOrDefault(note => note.Name_of_note == selectedName);                    // Габелла (тыкался-тыкался и каким-то фигом оно работает)
                    if (selectedNote != null)
                    {
                        TextBox1.Text = selectedNote.Name_of_note;
                        TextBox2.Text = selectedNote.Discription;
                    }
                }
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (List_of_Notes.SelectedItem != null)
            {
                string selectedNoteName = List_of_Notes.SelectedItem.ToString();
                Note selectedNote = List_of_notes.FirstOrDefault(note => note.Name_of_note == selectedNoteName);

                if (selectedNote != null)
                {
                    List_of_notes.Remove(selectedNote);

                    List_of_Notes_Show();

                    Converter_to_JSON.MySerealize(List_of_notes);

                    TextBox1.Text = "";
                    TextBox2.Text = "";
                }
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (List_of_Notes.SelectedItem != null)
            {
                string selectedNoteName = List_of_Notes.SelectedItem.ToString();

                // Находим выбранную заметку в списке List_of_notes
                Note selectedNote = List_of_notes.FirstOrDefault(note => note.Name_of_note == selectedNoteName);

                if (selectedNote != null)
                {
                    // Обновляем значения выбранной заметки
                    selectedNote.Name_of_note = TextBox1.Text;
                    selectedNote.Discription = TextBox2.Text;
                    selectedNote.Date_of_note = Date_Changer.Text;

                    // Сохраняем обновленный список в JSON файл
                    Converter_to_JSON.MySerealize(List_of_notes);

                    // Перезагружаем отображение в ListBox
                    List_of_Notes_Show();

                    TextBox1.Text = "";
                    TextBox2.Text = "";
                }
            }
        }
    }
}

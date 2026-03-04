using System.Collections.ObjectModel;

namespace ToDoMaui_Listview;

public partial class MainPage : ContentPage
{
    ObservableCollection<ToDoClass> todoList = new();
    int selectedId = -1;
    int autoId = 1;

    public MainPage()
    {
        InitializeComponent();
        todoLV.ItemsSource = todoList;
    }

    private void AddToDoItem(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(titleEntry.Text))
            return;

        todoList.Add(new ToDoClass
        {
            id = autoId++,
            title = titleEntry.Text,
            detail = detailsEditor.Text
        });

        ClearFields();
    }

    private void DeleteToDoItem(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int id = Convert.ToInt32(btn.ClassId);

        var item = todoList.FirstOrDefault(x => x.id == id);
        if (item != null)
            todoList.Remove(item);
    }

    private void TodoLV_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;

        var item = (ToDoClass)e.SelectedItem;

        selectedId = item.id;
        titleEntry.Text = item.title;
        detailsEditor.Text = item.detail;

        addBtn.IsVisible = false;
        editBtn.IsVisible = true;
        cancelBtn.IsVisible = true;
    }

    private void EditToDoItem(object sender, EventArgs e)
    {
        var item = todoList.FirstOrDefault(x => x.id == selectedId);

        if (item != null)
        {
            item.title = titleEntry.Text;
            item.detail = detailsEditor.Text;
        }

        CancelEdit(null, null);
    }

    private void CancelEdit(object sender, EventArgs e)
    {
        ClearFields();

        addBtn.IsVisible = true;
        editBtn.IsVisible = false;
        cancelBtn.IsVisible = false;

        todoLV.SelectedItem = null;
    }

    private void ClearFields()
    {
        titleEntry.Text = "";
        detailsEditor.Text = "";
    }
}

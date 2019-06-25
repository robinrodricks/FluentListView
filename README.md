# FluentListView
FluentListView is a C# wrapper around a .NET ListView, supporting model-bound lists, in-place item editing, drag and drop, icons, themes, trees & data grids, and much more.

FluentListView is designed to view a list of strongly-typed objects. You don't need to create `ListItem` objects, you just feed it your list and everything is handled internally.

It extracts certain properties from your objects, converts those properties to strings, and then displays those as list items.

-----------------------------------

![_images/ModelToScreenProcess.png](_images/ModelToScreenProcess.png)

*Diagram copyrighted © 2006-2019 by Phillip Piper.*

*All credits go to Phillip Piper for creating the original project [ObjectListView](http://objectlistview.sourceforge.net/).*

## Example

For example, let's define an object to hold the metadata of a file.

```cs
// define our list item class
public class FileObject {
	public string Name;
	public string Path;
	public DateTime DateCreated;
	public DateTime DateModified;
}
```

We can then use FluentListView to display these file objects. We'll also allow users to drag-and-drop files from Explorer into the list.

```cs
using Fluent;
using System.Windows.Forms;
using System.Drawing;


// create a list and add it into the form
var list = new FluentListView();
form.Controls.Add(list);


// set basic visual properties
list.Size = new Size(800, 200);
list.Theme = OLVTheme.VistaExplorer;
list.ItemFont = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);


// display the the file name as the list item label
list.Properties.Name = "Name";


// display some more properties from our objects as columns
list.ShowColumns = true;
list.Properties.Columns = new List<string>     { "DateCreated",  "DateModified",  "FullPath"  };
list.Properties.ColumnNames = new List<string> { "Date Created", "Date Modified", "Full Path" };


// set the default items
list.Items = new List<FileObject>();


// enable drag-and-drop files from Explorer
list.EnableDropFiles = true;
list.EnableDropOnLocations = DropTargetLocation.Background | DropTargetLocation.Item;
list.OnDroppedFiles = delegate (List<string> paths) {
	paths.ForEach(path => {
	
		// add the file to the list
		var item = new FileObject {
			Name = Path.GetFileName(path),
			Path = path,
			DateCreated = File.GetCreationTime(path),
			DateModified = File.GetLastWriteTime(path),
		};
		
		// quickly add the list item to the view
		list.AddItem(item);
	});
};
```

You can later use these properties to get info about the list items and selection.

```cs
// get the currently displayed items
var currentItems = ((List<FileObject>)list.Items);

// get the currently selected items
var selectedItems = list.SelectedItems.ToList<FileObject>();
```

## Basic API

- list.**Properties** - Which properties of your objects to display on the view.
   
   - list.**Properties.Name** - Which object property to display as the list item label. The property can be of any type.
   
   - list.**Properties.Icon** - Which object property to display as the icon. It must be a `Icon` or `Bitmap` property.
   
   - list.**Properties.Description** - Which object property to display as the description, just below the item label. The property can be of any type.
   
   - list.**Properties.Columns** - Which object properties to display as additional columns. The properties can be of any type.
   
   - list.**Properties.ColumnNames** - Friendly names to display as the column headers. Must be the same length as `Properties.Columns`. If you don't specify this, the `Properties.Columns` list is displayed as the column headers instead.

- list.**Items** - The items that are bound to this list. Set this to your list of objects. You only need to set this once. When adding and removing a single item, please call `AddItem` and `RemoveItem` instead of modifying this list directly, or call `Redraw` to update the view.

- list.**Theme** - Which visual theme to use to render items.

- list.**ItemFont** - The font used to display list items. Affects row height.

- list.**InnerList** - Gets the underlying AdvancedListView or FastListView UI control.

- list.**SelectedItem** - Gets or sets the selected item. Setting this property automatically scrolls to the item and highlights it.

- list.**SelectedItems** - Gets or sets the selected items. Setting this property highlights all the given items.


## Drag and Drop API

- list.**EnableDragDropItems** - Whether the list will use a SimpleDragSource to initiate drags.

- list.**EnableDrop** - Whether the list will use a SimpleDropSink to accept dropping items from other sources.

- list.**EnableDropFiles** - Whether the list will accept dropping of files from Explorer. You need to handle `OnDroppedFiles`.

- list.**EnableDropOnLocations** - You need to set this if you are using drag or drag-drop.

- list.**OnCanDrop** - You need to set this if you are using EnableDragDropItems or EnableDrop, but not if you are using EnableDropFiles.

- list.**OnDropped** - You need to set this if you are using EnableDragDropItems or EnableDrop, but not if you are using EnableDropFiles.

- list.**OnDroppedFiles** - You need to set this if you are using EnableDropFiles.


## Columns API

- list.**ShowColumns** - Used to control if additional columns are displayed based on `list.Properties.Columns`.

- list.**ColumnWidth** - The width of additional columns.


## Advanced API

You can also directly modify the underlying [AdvancedListView](https://github.com/robinrodricks/FluentListView/blob/master/ADVANCED.md) if you need these:

*  Sorting and grouping rows.
*  Editing cell values.
*  Other views (report, list, large and small icons).
*  Owner drawing, rendering animated graphics and images stored in a database.
*  Display a “list is empty” message.
*  Fancy tooltips for cells and headers.
*  Buttons in cells.
*  Checkboxes in any column as well as tri-state checkboxes.
*  [And much more...](https://github.com/robinrodricks/FluentListView/blob/master/ADVANCED.md)
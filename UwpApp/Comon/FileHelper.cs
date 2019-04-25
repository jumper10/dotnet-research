using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace UwpApp.Comon
{
    public static class FileHelper
    {
        public static async Task<StorageFile> SelectFileAsync(PickerLocationId locationId = PickerLocationId.Desktop, IList<string> filters = null)
        {
            var files = await SelectFilesAsync(locationId, filters);
            if (files != null && files.Any())
            {
                return files.FirstOrDefault();
            }
            return null;
        }

        public static async Task<IReadOnlyList<StorageFile>> SelectFilesAsync(PickerLocationId locationId = PickerLocationId.Desktop, IList<string> filters = null, bool singleMode = true)
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = locationId;
            if (filters == null)
                openPicker.FileTypeFilter.Add("*");
            else
            {
                foreach (var filter in filters)
                    openPicker.FileTypeFilter.Add(filter);
            }
            var files = new List<StorageFile>();
            if (singleMode)
            {

                files.Add(await openPicker.PickSingleFileAsync());
            }
            else
            {
                files.AddRange(await openPicker.PickMultipleFilesAsync());
            }
            return files;
        }
    }
}

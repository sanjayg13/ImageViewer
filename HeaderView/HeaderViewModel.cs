using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageViewer.Ui.Utilities;
using ImageViewer.Ui.Utilities.Events;
using Prism.Events;

namespace ImageViewer.HeaderView
{
    /// <summary>
    ///     View model
    /// </summary>
    public class HeaderViewModel : Observable
    {
        private readonly IEventAggregator _eventAggregator;
        private string _searchTag;
        private bool _isUndoEnabled;

        private ICommand _searchClickCommand;
        private ICommand _undoClickCommand;
        public string SearchTag
        {
            get => _searchTag;
            set => SetField(ref _searchTag, value);
        }

        public bool IsUndoEnabled
        {
            get => _isUndoEnabled;
            set => SetField(ref _isUndoEnabled, value);
        }

        public Stack<string> KeywordStack { get; set; }

        public ICommand SearchClickCommand
        {
            get => _searchClickCommand;
            set => SetField(ref _searchClickCommand, value);
        }

        public ICommand UndoClickCommand
        {
            get => _undoClickCommand;
            set => SetField(ref _undoClickCommand, value);
        }

        public HeaderViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            KeywordStack = new Stack<string>();
            SearchClickCommand = new Command(OnSearchButtonClick);
            UndoClickCommand = new Command(OnUndoButtonClick);
        }

        private void OnSearchButtonClick(object obj)
        {
            if (KeywordStack.Count > 0)
            {
                IsUndoEnabled = true;
            }
            KeywordStack.Push(SearchTag);
            _eventAggregator.GetEvent<InvokeImageSearchEvent>().Publish(SearchTag);
        }

        private void OnUndoButtonClick(object obj)
        {
            KeywordStack.Pop();
            if(KeywordStack.Count > 0)
            {
                _eventAggregator.GetEvent<InvokeLastSearchEvent>().Publish(KeywordStack.Peek());
                SearchTag = KeywordStack.Peek();
            }

            if (KeywordStack.Count < 2)
            {
                IsUndoEnabled = false;
            }
        }

    }
}

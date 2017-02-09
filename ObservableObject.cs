using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TransformsPrototype
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Event raised when a property of the view model changes value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the name of the expression member.
        /// </summary>
        /// <typeparam name="T">Member type.</typeparam>
        /// <param name="func">The function.</param>
        /// <returns>Expression member name.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nesting needed")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Needed expressions tree")]
        public static string GetExpressionMemberName<T>(Expression<Func<T>> func)
        {
            var memberExpr = func.Body as MemberExpression;

            Debug.Assert(memberExpr != null, "memberExpr != null");

            return memberExpr.Member.Name;
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <typeparam name="T">Member type.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "This is appropriate")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nesting needed")]
     
        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            RaisePropertyChanged(GetExpressionMemberName(propertyExpression));
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "This is appropriate")]

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Sets the value of the specified property if new value is not same as current also
        /// raising the PropertyChanged event. This method uses provided equality comparer for
        /// comparison of property value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="field">The property.</param>
        /// <param name="value">The new value.</param>
        /// <param name="comparer">The comparer to use for property value equality.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>
        /// True if property value was changed, otherwise False.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Appropriate here")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nesting needed")]
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Default parameters combined with named parameters is nice here. We do not think we will call this from other languages.")]
     
        protected bool SetProperty<T>(ref T field, T value, IEqualityComparer<T> comparer, [CallerMemberName] string propertyName = "")
        {
            VerifyPropertyName(propertyName);
            if (comparer == null)
            {
                comparer = EqualityComparer<T>.Default;
            }

            if (comparer.Equals(field, value))
            {
                return false;
            }

            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <typeparam name="T">The type of the value to set.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="comparer">The comparer.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// True if property value was changed, otherwise False.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Appropriate here")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nesting needed")]
      
        protected bool SetProperty<T>(ref T field, T value, IEqualityComparer<T> comparer, Expression<Func<T>> propertyExpression)
        {
            return SetProperty(ref field, value, comparer, GetExpressionMemberName(propertyExpression));
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <typeparam name="T">The type of the value to set.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// True if property value was changed, otherwise False.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Appropriate here")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Nesting needed")]
       
        protected bool SetProperty<T>(ref T field, T value, Expression<Func<T>> propertyExpression)
        {
            return SetProperty(ref field, value, GetExpressionMemberName(propertyExpression));
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <typeparam name="T">The type of the value to set.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>True if property value was changed, otherwise False.</returns>
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Appropriate here")]
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Default parameters combined with named parameters is nice here. We do not think we will call this from other languages.")]
        
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            return SetProperty(ref field, value, null, propertyName);
        }

        /// <summary>
        /// Verifies if the specified property exists on the view model.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || (!string.IsNullOrEmpty(propertyName) && GetType().GetProperty(propertyName) == null))
            {
                throw new ArgumentException("Property not found.", propertyName);
            }
        }
    }
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Marker interface")]
    public interface IViewModel : INotifyPropertyChanged
    {
    }

    public interface ILogger
    {
    }

    public interface IMessenger
    {
        
    }

    public abstract class ViewModelBase : ObservableObject, IViewModel
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelBase class.
        /// </summary>
        /// <param name="logger">The logger associated with the application.</param>
        protected ViewModelBase(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected ILogger Logger { get; private set; }

        /// <summary>
        /// Subscribes to the specified message and stores the token to be unsubscribed on clean up.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="messenger">The messenger.</param>
        /// <param name="action">The action.</param>
        protected void SubscribeToMessage<TMessage>(IMessenger messenger, Action<TMessage> action)
        {
            
        }

        /// <summary>
        /// Subscribes to the specified message in a private message stream and stores the token to be unsubscribed on clean up.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="messenger">The messenger.</param>
        /// <param name="streamKey">The stream key.</param>
        /// <param name="action">The action.</param>
        protected void SubscribeToMessage<TMessage>(IMessenger messenger, string streamKey, Action<TMessage> action)
        {
        
        }
    }

    public class DelegateCommand : ICommand
    {
        private readonly Func<object, bool> _canExecuteFunc;

        private readonly Action<object> _executeAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="executeAction">The execute action.</param>
        /// <param name="canExecuteFunc">The can execute func.</param>
        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
        
            _executeAction = executeAction;
            _canExecuteFunc = canExecuteFunc;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="executeAction">The execute action.</param>
        /// <param name="canExecuteFunc">The can execute func.</param>
        public DelegateCommand(Action executeAction, Func<bool> canExecuteFunc)
        {
          
            _executeAction = p => executeAction();
            _canExecuteFunc = p => canExecuteFunc();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand" /> class.
        /// </summary>
        /// <param name="executeAction">The execute action.</param>
        /// <param name="canExecuteFunc">The can execute func.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="logMessageFunc">The function which provides the message to be logged when the command is invoked from the UI.</param>
        public DelegateCommand(
            Action<object> executeAction,
            Func<object, bool> canExecuteFunc,
            ILogger logger,
            Func<object, string> logMessageFunc)
        {
            _executeAction = executeAction;
            _canExecuteFunc = canExecuteFunc;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand" /> class.
        /// </summary>
        /// <param name="executeAction">The execute action.</param>
        /// <param name="canExecuteFunc">The can execute func.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="logMessageFunc">The function which provides the message to be logged when the command is invoked from the UI.</param>
        public DelegateCommand(
            Action executeAction,
            Func<bool> canExecuteFunc,
            ILogger logger,
            Func<string> logMessageFunc)
        {
         
            _executeAction = p => executeAction();
            _canExecuteFunc = p => canExecuteFunc();
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// <c>true</c> if this command can be executed; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }

        public event EventHandler CanExecuteChanged;
        
    }
}
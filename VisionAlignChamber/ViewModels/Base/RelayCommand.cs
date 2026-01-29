using System;
using System.Windows.Input;

namespace VisionAlignChamber.ViewModels.Base
{
    /// <summary>
    /// MVVM 패턴의 기본 Command 클래스
    /// ICommand 구현을 제공하여 버튼 등의 Command 바인딩 지원
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Fields

        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// 항상 실행 가능한 Command 생성
        /// </summary>
        /// <param name="execute">실행할 액션</param>
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// 조건부 실행 가능한 Command 생성
        /// </summary>
        /// <param name="execute">실행할 액션</param>
        /// <param name="canExecute">실행 가능 여부 판단 함수</param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand

        /// <summary>
        /// 실행 가능 여부 변경 시 발생하는 이벤트
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Command 실행 가능 여부 확인
        /// </summary>
        /// <param name="parameter">Command 파라미터 (사용 안 함)</param>
        /// <returns>실행 가능하면 true</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        /// <summary>
        /// Command 실행
        /// </summary>
        /// <param name="parameter">Command 파라미터 (사용 안 함)</param>
        public void Execute(object parameter)
        {
            _execute();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// CanExecuteChanged 이벤트 발생
        /// UI에서 Command의 실행 가능 여부를 다시 확인하도록 함
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }

    /// <summary>
    /// 파라미터를 받는 Command 클래스
    /// </summary>
    /// <typeparam name="T">파라미터 타입</typeparam>
    public class RelayCommand<T> : ICommand
    {
        #region Fields

        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// 항상 실행 가능한 Command 생성
        /// </summary>
        /// <param name="execute">실행할 액션</param>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// 조건부 실행 가능한 Command 생성
        /// </summary>
        /// <param name="execute">실행할 액션</param>
        /// <param name="canExecute">실행 가능 여부 판단 함수</param>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand

        /// <summary>
        /// 실행 가능 여부 변경 시 발생하는 이벤트
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Command 실행 가능 여부 확인
        /// </summary>
        /// <param name="parameter">Command 파라미터</param>
        /// <returns>실행 가능하면 true</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            if (parameter == null && typeof(T).IsValueType)
                return false;

            return _canExecute((T)parameter);
        }

        /// <summary>
        /// Command 실행
        /// </summary>
        /// <param name="parameter">Command 파라미터</param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// CanExecuteChanged 이벤트 발생
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VisionAlignChamber.ViewModels.Base
{
    /// <summary>
    /// MVVM 패턴의 기본 ViewModel 클래스
    /// INotifyPropertyChanged 구현을 제공하여 UI 바인딩 지원
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        /// <summary>
        /// 속성 값 변경 시 발생하는 이벤트
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// PropertyChanged 이벤트 발생
        /// </summary>
        /// <param name="propertyName">변경된 속성 이름 (자동 설정)</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 속성 값 설정 및 변경 알림
        /// </summary>
        /// <typeparam name="T">속성 타입</typeparam>
        /// <param name="field">백킹 필드 참조</param>
        /// <param name="value">새 값</param>
        /// <param name="propertyName">속성 이름 (자동 설정)</param>
        /// <returns>값이 변경되었으면 true</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// 속성 값 설정 및 변경 알림 (추가 액션 실행)
        /// </summary>
        /// <typeparam name="T">속성 타입</typeparam>
        /// <param name="field">백킹 필드 참조</param>
        /// <param name="value">새 값</param>
        /// <param name="onChanged">값 변경 시 실행할 액션</param>
        /// <param name="propertyName">속성 이름 (자동 설정)</param>
        /// <returns>값이 변경되었으면 true</returns>
        protected bool SetProperty<T>(ref T field, T value, Action onChanged, [CallerMemberName] string propertyName = null)
        {
            if (!SetProperty(ref field, value, propertyName))
                return false;

            onChanged?.Invoke();
            return true;
        }

        /// <summary>
        /// 여러 속성의 변경 알림
        /// </summary>
        /// <param name="propertyNames">속성 이름들</param>
        protected void OnPropertiesChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                OnPropertyChanged(propertyName);
            }
        }

        #endregion

        #region Dispose Pattern

        private bool _disposed = false;

        /// <summary>
        /// 리소스 해제 여부
        /// </summary>
        public bool IsDisposed => _disposed;

        /// <summary>
        /// 리소스 해제
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 리소스 해제 구현
        /// </summary>
        /// <param name="disposing">관리 리소스 해제 여부</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // 관리 리소스 해제
                OnDisposing();
            }

            _disposed = true;
        }

        /// <summary>
        /// 파생 클래스에서 오버라이드하여 리소스 해제 구현
        /// </summary>
        protected virtual void OnDisposing()
        {
            // 파생 클래스에서 구현
        }

        /// <summary>
        /// 소멸자
        /// </summary>
        ~ViewModelBase()
        {
            Dispose(false);
        }

        #endregion
    }
}

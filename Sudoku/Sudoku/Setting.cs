namespace Sudoku.Properties
{
    // 이 클래스에서는 설정 클래스에서 특정 이벤트를 처리할 수 있습니다.:
    //  설정 값이 변경되기 전에 설정/변경 이벤트가 발생합니다.
    //  설정 값이 변경된 후 속성 변경 이벤트가 발생합니다.
    //  설정 값이 로드되면 설정/해제 이벤트가 발생합니다.
    //  설정 값을 저장하기 전에 설정/떨림 이벤트가 발생합니다.
    internal sealed partial class Settings
    {
        public Settings()
        {
            // // 설정을 저장하고 변경하기 위해 이벤트 핸들러를 추가하려면 아래의 라인을 수정합니다:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }

        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e)
        {
            // 여기서 SettingCangE 이벤트/이벤트를 처리하려면 코드를 추가하십시오.
        }

        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 여기서 SettingCangE 이벤트/이벤트를 처리하려면 코드를 추가하십시오.
        }
    }
}
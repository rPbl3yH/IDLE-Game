using App.Core;

namespace Modules.Tutorial
{
    public class TutorialSaveMediator : GameMediator<int, TutorialState>
    {
        protected override void SetupFromData(TutorialState service, int data)
        {
            for (int i = 0; i <= data; i++)
            {
                service.FinishStep();
            }
        }

        protected override void SetupByDefault(TutorialState service)
        {
            service.NextStep();
        }

        protected override int ConvertToData(TutorialState service)
        {
            return (int)service.CurrentStep;
        }
    }
}
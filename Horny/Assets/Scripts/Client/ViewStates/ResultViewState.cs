namespace Client.ViewStates
{
    public class ResultViewState : BaseViewState
    {
        public override void OnEnter()
        {
            var resultScreen = Context.Screens.ResultScreen; 
            Context.Screens.SetResultView();
            
            resultScreen.Menu.onClick.RemoveAllListeners();
            resultScreen.Menu.onClick.AddListener(() =>
            {
                Context.AppModel.DeleteWorld();
                SetState(new ChooseWorldViewState());
            });
            
            resultScreen.Restart.onClick.RemoveAllListeners();
            resultScreen.Restart.onClick.AddListener(() =>
            {
                Context.AppModel.CreateWorld(Context.AppModel.World.WorldId);
                SetState(new GameViewState());
            });
            
            resultScreen.NextLevel.onClick.RemoveAllListeners();
            resultScreen.NextLevel.onClick.AddListener(() =>
            {
                Context.AppModel.CreateWorld(Context.AppModel.World.WorldId + 1);
                SetState(new GameViewState());
            });
            
            var score = Context.GameState.WorldState.Score;
            var rules = Context.GameState.Rules;
            var stars = rules.GetStarByScore(score);
            resultScreen.SetResults(score, stars, Context.AppModel.World.WorldId);
        }
        
        public override void PreModelUpdate()
        {
            
        }

        public override void PostModelUpdate()
        {
            
        }
    }
}

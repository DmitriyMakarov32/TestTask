using System.Threading.Tasks.Dataflow;

namespace TT.Shared.Helpers;

public class TplHelper<TIn, TOut>
	{
		private List<(IDataflowBlock Block, bool IsAsync, bool IsMany)> _steps = new ();

		public async Task Execute(TIn input)
		{
			try
			{
				var firstStep = _steps[0].Block as ITargetBlock<TIn>;
				firstStep!.Post(input);
				firstStep.Complete();

				var lastStep = _steps.Last();
				await lastStep.Block.Completion;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

	        public TplHelper<TIn, TOut> AddStepAsync<TLocalIn, TLocalOut>(Func<TLocalIn, Task<TLocalOut>> stepFunc)
	        {
	            if (_steps.Count == 0)
	            {
	                var step =
	                    new TransformBlock<TLocalIn, TLocalOut>(stepFunc);
	                _steps.Add((step, IsAsync: false, IsMany: false));
	            }
	            else
	            {
	                var lastStep = _steps.Last();
	                ExecutionDataflowBlockOptions options;
	                if (lastStep.IsMany)
	                {
		                options = new ExecutionDataflowBlockOptions
		                {
			                CancellationToken = default,
			                EnsureOrdered = false,
			                MaxDegreeOfParallelism = 30,
			                MaxMessagesPerTask = 1
		                };
	                }
	                else
	                {

		                options = new ExecutionDataflowBlockOptions
		                {
			                CancellationToken = default,
			                MaxMessagesPerTask = 1
		                };
	                }
                    var step = new TransformBlock<TLocalIn, TLocalOut>(stepFunc, options);
                    var targetBlock = (lastStep.Block as ISourceBlock<TLocalIn>);
                    targetBlock!.LinkTo(step, new DataflowLinkOptions {PropagateCompletion = true});
                    _steps.Add((step, IsAsync: false, IsMany: false));
	            }

	            return this;
	        }

	        public TplHelper<TIn, TOut> AddManyStepAsync<TLocalIn, TLocalOut>(Func<TLocalIn, Task<IEnumerable<TLocalOut>>> stepFunc)
	        {
		        ExecutionDataflowBlockOptions options = new ExecutionDataflowBlockOptions
		        {
			        CancellationToken = default
		        };
		        if (_steps.Count == 0)
		        {
			        var step =
				        new TransformManyBlock<TLocalIn, TLocalOut>(stepFunc, options);
			        _steps.Add((step, IsAsync: false, IsMany: true));
		        }
		        else
		        {
			        var lastStep = _steps.Last();
			        var step = new TransformManyBlock<TLocalIn, TLocalOut>(async (input) =>
				        await stepFunc(input), options);
			        var targetBlock = (lastStep.Block as ISourceBlock<TLocalIn>);
			        targetBlock!.LinkTo(step, new DataflowLinkOptions {PropagateCompletion = true});
			        _steps.Add((step, IsAsync: false, IsMany: true));
		        }

		        return this;
	        }

	        public TplHelper<TIn, TOut> AddFinalStep<TLocalIn>(Func<TLocalIn, Task> stepFunc)
	        {
		        var options = new ExecutionDataflowBlockOptions
		        {
			        CancellationToken = default,
			        MaxMessagesPerTask = 1
		        };

	            var lastStep = _steps.Last();
	            var callBackStep = new ActionBlock<TLocalIn>(stepFunc, options);
	            var targetBlock = (lastStep.Block as ISourceBlock<TLocalIn>);
	            targetBlock!.LinkTo(callBackStep, new DataflowLinkOptions {PropagateCompletion = true});
	            _steps.Add((callBackStep, IsAsync: false, IsMany: false));

	            return this;
	        }
	}
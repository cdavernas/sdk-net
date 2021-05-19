﻿using FluentValidation;
using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Services.Validation
{

    /// <summary>
    /// Represents the service used to validate <see cref="ActionDefinition"/>s
    /// </summary>
    public class ActionDefinitionValidator
        : AbstractValidator<ActionDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="ActionDefinitionValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="ActionDefinition"/>s to validate belong to</param>
        public ActionDefinitionValidator(WorkflowDefinition workflow)
        {
            this.Workflow = workflow;
            this.RuleFor(a => a.Event)
                .NotNull()
                .When(a => a.Function == null)
                .WithErrorCode($"{nameof(ActionDefinition)}.{nameof(ActionDefinition.Event)}");
            this.RuleFor(a => a.Event)
                .SetValidator(new EventReferenceValidator(this.Workflow))
                .When(a => a.Event != null)
                .WithErrorCode($"{nameof(ActionDefinition)}.{nameof(ActionDefinition.Event)}");
            this.RuleFor(a => a.Function)
                .NotNull()
                .When(a => a.Event == null)
                .WithErrorCode($"{nameof(ActionDefinition)}.{nameof(ActionDefinition.Function)}");
            this.RuleFor(a => a.Function)
                .SetValidator(new FunctionReferenceValidator(this.Workflow))
                .When(a => a.Function != null)
                .WithErrorCode($"{nameof(ActionDefinition)}.{nameof(ActionDefinition.Function)}");
        }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> the <see cref="ActionDefinition"/>s to validate belong to
        /// </summary>
        protected WorkflowDefinition Workflow { get; }

    }

}

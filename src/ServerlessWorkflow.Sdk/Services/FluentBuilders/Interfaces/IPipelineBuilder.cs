﻿/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="StateDefinition"/> charts
    /// </summary>
    public interface IPipelineBuilder
    {

        /// <summary>
        /// Adds the specified <see cref="EventDefinition"/> to the pipeline
        /// </summary>
        /// <param name="eventSetup">The <see cref="Action{T}"/> used to setup the <see cref="EventDefinition"/> to add</param>
        /// <returns>A new <see cref="EventDefinition"/></returns>
        EventDefinition AddEvent(Action<IEventBuilder> eventSetup);

        /// <summary>
        /// Adds the specified <see cref="EventDefinition"/> to the pipeline
        /// </summary>
        /// <param name="e">The <see cref="EventDefinition"/> to add</param>
        /// <returns>A new <see cref="EventDefinition"/></returns>
        EventDefinition AddEvent(EventDefinition e);

        /// <summary>
        /// Adds the specified <see cref="FunctionDefinition"/> to the pipeline
        /// </summary>
        /// <param name="functionSetup">The <see cref="Action{T}"/> used to setup the <see cref="FunctionDefinition"/> to add</param>
        /// <returns>A new <see cref="FunctionDefinition"/></returns>
        FunctionDefinition AddFunction(Action<IFunctionBuilder> functionSetup);

        /// <summary>
        /// Adds the specified <see cref="FunctionDefinition"/> to the pipeline
        /// </summary>
        /// <param name="function">The <see cref="FunctionDefinition"/> to add</param>
        /// <returns>A new <see cref="FunctionDefinition"/></returns>
        FunctionDefinition AddFunction(FunctionDefinition function);

        /// <summary>
        /// Adds the specified <see cref="StateDefinition"/> to the pipeline
        /// </summary>
        /// <param name="stateSetup">The <see cref="Func{T, TResult}"/> used to build and configure the <see cref="StateDefinition"/> to add</param>
        /// <returns>A new <see cref="StateDefinition"/></returns>
        StateDefinition AddState(Func<IStateBuilderFactory, IStateBuilder> stateSetup);

        /// <summary>
        /// Adds the specified <see cref="StateDefinition"/> to the pipeline
        /// </summary>
        /// <param name="state">The <see cref="StateDefinition"/> to add</param>
        /// <returns>The newly added <see cref="StateDefinition"/></returns>
        StateDefinition AddState(StateDefinition state);

        /// <summary>
        /// Transitions to the specified <see cref="StateDefinition"/>
        /// </summary>
        /// <param name="stateSetup">An <see cref="Action{T}"/> used to setup the <see cref="StateDefinition"/> to transition to</param>
        /// <returns>A new <see cref="IStateBuilder{TState}"/> used to configure the <see cref="StateDefinition"/> to transition to</returns>
        IPipelineBuilder Then(Func<IStateBuilderFactory, IStateBuilder> stateSetup);

        /// <summary>
        /// Transitions to the specified <see cref="StateDefinition"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to transition to</param>
        /// <param name="stateSetup">An <see cref="Action{T}"/> used to setup the <see cref="StateDefinition"/> to transition to</param>
        /// <returns>A new <see cref="IStateBuilder{TState}"/> used to configure the <see cref="StateDefinition"/> to transition to</returns>
        IPipelineBuilder Then(string name, Func<IStateBuilderFactory, IStateBuilder> stateSetup);

        /// <summary>
        /// Configure the <see cref="StateDefinition"/> to end the workflow upon completion
        /// </summary>
        /// <param name="stateSetup">An <see cref="Action{T}"/> used to setup the <see cref="StateDefinition"/> to end the workflow with</param>
        /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
        IWorkflowBuilder EndsWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup);

        /// <summary>
        /// Configure the <see cref="StateDefinition"/> to end the workflow upon completion
        /// </summary>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to end the workflow execution with</param>
        /// <param name="stateSetup">An <see cref="Action{T}"/> used to setup the <see cref="StateDefinition"/> to end the workflow with</param>
        /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
        IWorkflowBuilder EndsWith(string name, Func<IStateBuilderFactory, IStateBuilder> stateSetup);

        /// <summary>
        /// Configures the last <see cref="StateDefinition"/> to end the workflow upon completion
        /// </summary>
        /// <returns>The configured <see cref="IPipelineBuilder"/></returns>
        IWorkflowBuilder End();

        /// <summary>
        /// Builds the pipeline
        /// </summary>
        /// <returns>A new <see cref="IEnumerable{T}"/> that contains the <see cref="StateDefinition"/>s the pipeline is made out of</returns>
        IEnumerable<StateDefinition> Build();

    }

}

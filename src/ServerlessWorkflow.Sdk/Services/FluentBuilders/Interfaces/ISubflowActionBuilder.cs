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

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Defines the fundamentals of a service used to build <see cref="ActionDefinition"/>s of type <see cref="ActionType.Subflow"/>
    /// </summary>
    public interface ISubflowActionBuilder
    {

        /// <summary>
        /// Configures the <see cref="SubflowReference"/> to run the latest version of the specified <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <returns>The configured <see cref="ISubflowActionBuilder"/></returns>
        ISubflowActionBuilder LatestVersion();

        /// <summary>
        /// Configures the <see cref="SubflowReference"/> to run the <see cref="WorkflowDefinition"/> with the specified version
        /// </summary>
        /// <param name="version">The version of the <see cref="WorkflowDefinition"/> to run</param>
        /// <returns>The configured <see cref="ISubflowActionBuilder"/></returns>
        ISubflowActionBuilder Version(string version);

        /// <summary>
        /// Configures the <see cref="SubflowReference"/> to wait for the referenced <see cref="WorkflowDefinition"/>'s execution
        /// </summary>
        /// <param name="wait">A boolean indicating whether or not to wait for the referenced <see cref="WorkflowDefinition"/>'s execution</param>
        /// <returns>The configured <see cref="ISubflowActionBuilder"/></returns>
        ISubflowActionBuilder WaitForCompletion(bool wait = true);

    }

}

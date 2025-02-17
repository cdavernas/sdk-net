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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using ServerlessWorkflow.Sdk.Services.FluentBuilders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents the definition of a Serverless Workflow
    /// </summary>
    public class WorkflowDefinition
    {

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s unique identifier
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s domain-specific workflow identifier
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s name
        /// </summary>
        [Newtonsoft.Json.JsonRequired]
        [Required]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s version
        /// </summary>
        [Newtonsoft.Json.JsonRequired]
        [Required]
        public virtual string Version { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="System.Version"/> of the Serverless Workflow schema to use
        /// </summary>
        public virtual string SpecVersion { get; set; } = typeof(WorkflowDefinition).Assembly.GetName().Version.ToString(2);

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="WorkflowDefinition"/>'s data input <see cref="JSchema"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "dataInputSchema")]
        [System.Text.Json.Serialization.JsonPropertyName("dataInputSchema")]
        [YamlMember(Alias = "dataInputSchema")]
        protected virtual JToken DataInputSchemaToken { get; set; }

        private JSchema _DataInputSchema;
        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s data input <see cref="JSchema"/>
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual JSchema DataInputSchema
        {
            get
            {
                if (this._DataInputSchema == null
                    && this.DataInputSchemaToken != null)
                {
                    if (this.DataInputSchemaToken.Type == JTokenType.String)
                        this._DataInputSchema = new ExternalJSchema(this.DataInputSchemaToken.ToObject<Uri>());
                    else
                        this._DataInputSchema = this.DataInputSchemaToken.ToObject<JSchema>();
                }
                return this._DataInputSchema;
            }
            set
            {
                if (value == null)
                {
                    this._DataInputSchema = null;
                    this.DataInputSchemaToken = null;
                    return;
                }
                this._DataInputSchema = value;
                this.DataInputSchemaToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="Uri"/> to the file that defines the <see cref="WorkflowDefinition"/>'s data input <see cref="JSchema"/> 
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual Uri DataInputSchemaUri
        {
            get
            {
                if (this.DataInputSchema == null
                    || this._DataInputSchema is not ExternalJSchema externalSchema)
                    return null;
                return externalSchema.DefinitionUri;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this._DataInputSchema = new ExternalJSchema(value);
                this.DataInputSchemaToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the language the <see cref="WorkflowDefinition"/>'s expressions are expressed in
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "expressionLang")]
        [System.Text.Json.Serialization.JsonPropertyName("expressionLang")]
        [YamlMember(Alias = "expressionLang")]
        [Required]
        public virtual string ExpressionLanguage { get; set; } = "jq";

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s annotations
        /// </summary>
        public virtual List<string> Annotations { get; set; } = new List<string>();

        /// <summary>
        /// Gets/sets the object used to configure the <see cref="WorkflowDefinition"/>'s execution timeout
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "execTimeout")]
        [System.Text.Json.Serialization.JsonPropertyName("execTimeout")]
        [YamlMember(Alias = "execTimeout")]
        public virtual ExecutionTimeoutDefinition ExecutionTimeout { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that defines the <see cref="WorkflowDefinition"/>'s start
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "start")]
        [System.Text.Json.Serialization.JsonPropertyName("start")]
        [YamlMember(Alias = "start")]
        protected virtual JToken StartToken { get; set; }

        private StartDefinition _Start;
        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s <see cref="StartDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual StartDefinition Start
        {
            get
            {
                if (this._Start == null
                    && this.StartToken != null)
                {
                    if (this.StartToken.Type == JTokenType.String)
                        this._Start = new StartDefinition() { StateName = this.StartToken.ToObject<string>() };
                    else
                        this._Start = this.StartToken.ToObject<StartDefinition>();
                }
                return this._Start;
            }
            set
            {
                if (value == null)
                {
                    this._Start = null;
                    this.StartToken = null;
                    return;
                }
                this._Start = value;
                this.StartToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not to keep instances of the <see cref="WorkflowDefinition"/> active event if there are no active execution paths. Instance can be terminated via 'terminate end definition' or reaching defined 'execTimeout'
        /// </summary>
        public virtual bool KeepActive { get; set; } = false;

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s metadata
        /// </summary>
        public virtual JObject Metadata { get; set; } = new JObject();

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="EventDefinition"/> collection
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "events")]
        [System.Text.Json.Serialization.JsonPropertyName("events")]
        [YamlMember(Alias = "events")]
        protected virtual JToken EventsToken { get; set; }

        private List<EventDefinition> _Events;
        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="EventDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual List<EventDefinition> Events
        {
            get
            {
                if (this._Events == null
                    && this.EventsToken != null)
                {
                    if (this.EventsToken.Type == JTokenType.String)
                        this._Events = new ExternalDefinitionCollection<EventDefinition>(this.EventsToken.ToObject<Uri>());
                    else
                        this._Events = this.EventsToken.ToObject<List<EventDefinition>>();
                }
                if (this._Events == null)
                    this._Events = new List<EventDefinition>();
                return this._Events;
            }
            set
            {
                if (value == null)
                {
                    this._Events = null;
                    this.EventsToken = null;
                    return;
                }
                this._Events = value;
                this.EventsToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="Uri"/> to the file that defines the <see cref="WorkflowDefinition"/>'s <see cref="EventDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual Uri EventsUri
        {
            get
            {
                if (this.Events == null
                    || this._Events is not ExternalDefinitionCollection<EventDefinition> events)
                    return null;
                return events.DefinitionUri;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this._Events = new ExternalDefinitionCollection<EventDefinition>(value);
                this.EventsToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="FunctionDefinition"/> collection
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "functions")]
        [System.Text.Json.Serialization.JsonPropertyName("functions")]
        [YamlMember(Alias = "functions")]
        protected virtual JToken FunctionsToken { get; set; }

        private List<FunctionDefinition> _Functions;
        /// <summary>
        /// Gets/sets an <see cref="IEnumerable{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="EventDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual List<FunctionDefinition> Functions
        {
            get
            {
                if (this._Functions == null
                    && this.FunctionsToken != null)
                {
                    if (this.FunctionsToken.Type == JTokenType.String)
                        this._Functions = new ExternalDefinitionCollection<FunctionDefinition>(this.FunctionsToken.ToObject<Uri>());
                    else
                        this._Functions = this.FunctionsToken.ToObject<List<FunctionDefinition>>();
                }
                if (this._Functions == null)
                    this._Functions = new List<FunctionDefinition>();
                return this._Functions;
            }
            set
            {
                if (value == null)
                {
                    this._Functions = null;
                    this.FunctionsToken = null;
                    return;
                }
                this._Functions = value;
                this.FunctionsToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="Uri"/> to the file that defines the <see cref="WorkflowDefinition"/>'s <see cref="EventDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual Uri FunctionsUri
        {
            get
            {
                if (this.Functions == null
                    || this._Functions is not ExternalDefinitionCollection<FunctionDefinition> functions)
                    return null;
                return functions.DefinitionUri;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this._Functions = new ExternalDefinitionCollection<FunctionDefinition>(value);
                this.FunctionsToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="RetryStrategyDefinition"/> collection
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "retries")]
        [System.Text.Json.Serialization.JsonPropertyName("retries")]
        [YamlMember(Alias = "retries")]
        protected virtual JToken RetriesToken { get; set; }

        private List<RetryStrategyDefinition> _Retries;
        /// <summary>
        /// Gets/sets an <see cref="IEnumerable{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="RetryStrategyDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual List<RetryStrategyDefinition> Retries
        {
            get
            {
                if (this._Retries == null
                    && this.RetriesToken != null)
                {
                    if (this.RetriesToken.Type == JTokenType.String)
                        this._Retries = new ExternalDefinitionCollection<RetryStrategyDefinition>(this.RetriesToken.ToObject<Uri>());
                    else
                        this._Retries = this.RetriesToken.ToObject<List<RetryStrategyDefinition>>();
                }
                if (this._Retries == null)
                    this._Retries = new List<RetryStrategyDefinition>();
                return this._Retries;
            }
            set
            {
                if (value == null)
                {
                    this._Retries = null;
                    this.RetriesToken = null;
                    return;
                }
                this._Retries = value;
                this.RetriesToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="Uri"/> to the file that defines the <see cref="WorkflowDefinition"/>'s <see cref="RetryStrategyDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual Uri RetriesUri
        {
            get
            {
                if (this.Retries == null
                    || this._Retries is not ExternalDefinitionCollection<RetryStrategyDefinition> retries)
                    return null;
                return retries.DefinitionUri;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this._Retries = new ExternalDefinitionCollection<RetryStrategyDefinition>(value);
                this.RetriesToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="IEnumerable{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="StateDefinition"/>s
        /// </summary>
        public virtual List<StateDefinition> States { get; set; } = new List<StateDefinition>();

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="WorkflowDefinition"/>'s constants
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "constants")]
        [System.Text.Json.Serialization.JsonPropertyName("constants")]
        [YamlMember(Alias = "constants")]
        protected virtual JToken ConstantsToken { get; set; }

        private JObject _Constants;
        /// <summary>
        /// Gets/sets an <see cref="JObject"/> containing the <see cref="WorkflowDefinition"/>'s constants, which are globally accessible, read-only variables
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual JObject Constants
        {
            get
            {
                if (this._Constants == null
                    && this.ConstantsToken != null)
                {
                    if (this.ConstantsToken.Type == JTokenType.String)
                        this._Constants = new ExternalDefinition(this.ConstantsToken.ToObject<Uri>());
                    else
                        this._Constants = (JObject)this.ConstantsToken;
                }
                if (this._Constants == null)
                    this._Constants = new JObject();
                return this._Constants;
            }
            set
            {
                if (value == null)
                {
                    this._Constants = null;
                    this.ConstantsToken = null;
                    return;
                }
                this._Constants = value;
                this.ConstantsToken = value;
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="Uri"/> to the file that defines the <see cref="WorkflowDefinition"/>'s constants
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual Uri ConstantsUri
        {
            get
            {
                if (this.Constants == null
                    || this._Constants is not ExternalDefinition constants)
                    return null;
                return constants.DefinitionUri;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this._Constants = new ExternalDefinition(value);
                this.ConstantsToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="WorkflowDefinition"/>'s secrets
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "secrets")]
        [System.Text.Json.Serialization.JsonPropertyName("secrets")]
        [YamlMember(Alias = "secrets")]
        protected virtual JToken SecretsToken { get; set; }

        private List<string> _Secrets;
        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s secrets
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual List<string> Secrets
        {
            get
            {
                if (this._Secrets == null
                    && this.SecretsToken != null)
                {
                    if (this.SecretsToken.Type == JTokenType.String)
                        this._Secrets = new ExternalDefinitionCollection<string>(this.SecretsToken.ToObject<Uri>());
                    else
                        this._Secrets = this.SecretsToken.ToObject<List<string>>();
                }
                if (this._Secrets == null)
                    this._Secrets = new List<string>();
                return this._Secrets;
            }
            set
            {
                if (value == null)
                {
                    this._Secrets = null;
                    this.SecretsToken = null;
                    return;
                }
                this._Secrets = value;
                this.SecretsToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="Uri"/> to the file that defines the <see cref="WorkflowDefinition"/>'s secrets
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual Uri SecretshUri

        {
            get
            {
                if (this.Secrets == null
                    || this._Secrets is not ExternalDefinitionCollection<string> secrets)
                    return null;
                return secrets.DefinitionUri;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this._Secrets = new ExternalDefinitionCollection<string>(value);
                this.AuthToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="AuthenticationDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "auth")]
        [System.Text.Json.Serialization.JsonPropertyName("auth")]
        [YamlMember(Alias = "auth")]
        protected virtual JToken AuthToken { get; set; }

        private List<AuthenticationDefinition> _Auth;
        /// <summary>
        /// Gets/sets an <see cref="JObject"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="AuthenticationDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual List<AuthenticationDefinition> Auth
        {
            get
            {
                if (this._Auth == null
                    && this.AuthToken != null)
                {
                    if (this.AuthToken.Type == JTokenType.String)
                        this._Auth = new ExternalDefinitionCollection<AuthenticationDefinition>(this.AuthToken.ToObject<Uri>());
                    else
                        this._Auth = this.AuthToken.ToObject<List<AuthenticationDefinition>>();
                }
                if (this._Auth == null)
                    this._Auth = new List<AuthenticationDefinition>();
                return this._Auth;
            }
            set
            {
                if (value == null)
                {
                    this._Auth = null;
                    this.AuthToken = null;
                    return;
                }
                this._Auth = value;
                this.AuthToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="Uri"/> to the file that defines the <see cref="WorkflowDefinition"/>'s <see cref="AuthenticationDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual Uri AuthUri

        {
            get
            {
                if (this.Auth == null
                    || this._Auth is not ExternalDefinitionCollection<AuthenticationDefinition> auth)
                    return null;
                return auth.DefinitionUri;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this._Auth = new ExternalDefinitionCollection<AuthenticationDefinition>(value);
                this.AuthToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets the <see cref="StateDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to get</param>
        /// <returns>The <see cref="StateDefinition"/> with the specified name, if any</returns>
        public virtual StateDefinition GetState(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return this.States?.FirstOrDefault(s => s.Name == name);
        }

        /// <summary>
        /// Attempts to retrieve the <see cref="StateDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to retrieve</param>
        /// <param name="state">The <see cref="StateDefinition"/> with the specified name, if any</param>
        /// <returns>A boolean indicating whether or not a <see cref="StateDefinition"/> with the specified name could be found</returns>
        public virtual bool TryGetState(string name, out StateDefinition state)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            state = this.GetState(name);
            return state != null;
        }

        /// <summary>
        /// Gets the <see cref="StateDefinition"/> with the specified name
        /// </summary>
        /// <typeparam name="TState">The expected type of the <see cref="StateDefinition"/> with the specified name</typeparam>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to get</param>
        /// <returns>The <see cref="StateDefinition"/> with the specified name, if any</returns>
        public virtual TState GetState<TState>(string name)
            where TState : StateDefinition
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return this.GetState(name) as TState;
        }

        /// <summary>
        /// Attempts to retrieve the <see cref="StateDefinition"/> with the specified name
        /// </summary>
        /// <typeparam name="TState">The expected type of the <see cref="StateDefinition"/> with the specified name</typeparam>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to retrieve</param>
        /// <param name="state">The <see cref="StateDefinition"/> with the specified name, if any</param>
        /// <returns>A boolean indicating whether or not a <see cref="StateDefinition"/> with the specified name could be found</returns>
        public virtual bool TryGetState<TState>(string name, out TState state)
            where TState : StateDefinition
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            state = this.GetState<TState>(name);
            return state != null;
        }

        /// <summary>
        /// Gets the <see cref="EventDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="EventDefinition"/> to get</param>
        /// <returns>The <see cref="EventDefinition"/> with the specified name, if any</returns>
        public virtual EventDefinition GetEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return this.Events?.FirstOrDefault(e => e.Name == name);
        }

        /// <summary>
        /// Attempts to retrieve the <see cref="EventDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="EventDefinition"/> to retrieve</param>
        /// <param name="e">The <see cref="EventDefinition"/> with the specified name, if any</param>
        /// <returns>A boolean indicating whether or not a <see cref="EventDefinition"/> with the specified name could be found</returns>
        public virtual bool TryGetEvent(string name, out EventDefinition e)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            e = this.GetEvent(name);
            return e != null;
        }

        /// <summary>
        /// Gets the <see cref="FunctionDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="FunctionDefinition"/> to get</param>
        /// <returns>The <see cref="FunctionDefinition"/> with the specified name, if any</returns>
        public virtual FunctionDefinition GetFunction(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return this.Functions?.FirstOrDefault(e => e.Name == name);
        }

        /// <summary>
        /// Attempts to retrieve the <see cref="FunctionDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="FunctionDefinition"/> to retrieve</param>
        /// <param name="function">The <see cref="FunctionDefinition"/> with the specified name, if any</param>
        /// <returns>A boolean indicating whether or not a <see cref="FunctionDefinition"/> with the specified name could be found</returns>
        public virtual bool TryGetFunction(string name, out FunctionDefinition function)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            function = this.GetFunction(name);
            return function != null;
        }

        /// <summary>
        /// Gets the <see cref="AuthenticationDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="AuthenticationDefinition"/> to get</param>
        /// <returns>The <see cref="AuthenticationDefinition"/> with the specified name, if any</returns>
        public virtual AuthenticationDefinition GetAuthentication(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return this.Auth?.FirstOrDefault(e => e.Name == name);
        }

        /// <summary>
        /// Attempts to retrieve the <see cref="AuthenticationDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="AuthenticationDefinition"/> to retrieve</param>
        /// <param name="authentication">The <see cref="AuthenticationDefinition"/> with the specified name, if any</param>
        /// <returns>A boolean indicating whether or not a <see cref="AuthenticationDefinition"/> with the specified name could be found</returns>
        public virtual bool TryGetAuthentication(string name, out AuthenticationDefinition authentication)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            authentication = this.GetAuthentication(name);
            return authentication != null;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{ this.Id} {this.Version}";
        }

        /// <summary>
        /// Creates a new <see cref="IWorkflowBuilder"/> used to build a new <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="WorkflowDefinition"/> to create</param>
        /// <param name="name">The name of the <see cref="WorkflowDefinition"/> to create</param>
        /// <param name="version">The version of the <see cref="WorkflowDefinition"/> to create</param>
        /// <returns>A new <see cref="IWorkflowBuilder"/></returns>
        public static IWorkflowBuilder Create(string id, string name, string version)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentNullException(nameof(version));
            return new WorkflowBuilder()
                .WithId(id)
                .WithName(name)
                .WithVersion(version);
        }

    }

}

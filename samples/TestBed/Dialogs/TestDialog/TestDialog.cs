using System.Collections.Generic;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Adaptive;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Actions;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Conditions;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Generators;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Input;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Recognizers;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Templates;
using Microsoft.Bot.Builder.LanguageGeneration;

namespace Microsoft.BotBuilderSamples
{
    public class TestDialog : ComponentDialog
    {
        private Templates _lgFile;

        public TestDialog()
            : base(nameof(TestDialog))
        {
            var testDialog = new AdaptiveDialog("rootDialog")
            {
                AutoEndDialog = false,
                Generator = new TemplateEngineLanguageGenerator(),
                Recognizer = new RegexRecognizer()
                {
                    Intents = new List<IntentPattern>()
                    {
                        new IntentPattern()
                        {
                            Intent = "why",
                            Pattern = "why"
                        },
                        new IntentPattern()
                        {
                            Intent = "no",
                            Pattern = "no"
                        }
                    }
                },
                Triggers = new List<OnCondition>()
                {
                    new OnBeginDialog()
                    {
                        Actions = new List<Dialog>()
                        {
                            new SendActivity("In profile dialog..."),
                            new TextInput()
                            {
                                Id = "askForName",
                                Prompt = new ActivityTemplate("What is your name?"),
                                Property = "user.name"
                            },
                            new SendActivity("I have ${user.name}"),
                            new TextInput()
                            {
                                Id = "askForAge",
                                Prompt = new ActivityTemplate("What is your age?"),
                                Property = "user.age"
                            },
                            new SendActivity("I have ${user.age}")
                        }
                    },
                    new OnIntent()
                    {
                        Intent = "why",
                        Condition = "contains(dialogContext.stack, 'askForName')",
                        Actions = new List<Dialog>()
                        {
                            new SendActivity("I need your name to address you correctly"),
                        }
                    },
                    new OnIntent()
                    {
                        Intent = "why",
                        Condition = "contains(dialogContext.stack, 'askForAge')",
                        Actions = new List<Dialog>()
                        {
                            new SendActivity("I need your age to provide relevant product recommendations")
                        }
                    },
                    new OnIntent()
                    {
                        Intent = "why",
                        Actions = new List<Dialog>()
                        {
                            new SendActivity("I need your information to complete the sample..")
                        }
                    },
                    new OnIntent()
                    {
                        Intent = "no",
                        Actions = new List<Dialog>()
                        {
                            new SetProperties()
                            {
                                Assignments = new List<PropertyAssignment>()
                                {
                                    new PropertyAssignment()
                                    {
                                        Property = "user.name",
                                        Value = "Human"
                                    },
                                    new PropertyAssignment()
                                    {
                                        Property = "user.age",
                                        Value = "30"
                                    }
                                }
                            }
                        }
                    },
                    new OnIntent()
                    {
                       Intent = "no",
                       Condition = "contains(dialogContext.stack, 'askForName')",
                       Actions = new List<Dialog>()
                       {
                           new SetProperty()
                           {
                               Property = "user.name",
                               Value = "Human"
                           }
                       }
                    },
                    new OnIntent()
                    {
                       Intent = "no",
                       Condition = "contains(dialogContext.stack, 'askForAge')",
                       Actions = new List<Dialog>()
                       {
                           new SetProperty()
                           {
                               Property = "user.age",
                               Value = "30"
                           }
                       }
                    }
                }
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(testDialog);

            // The initial child dialog to run.
            InitialDialogId = "rootDialog";
        }
    }
}

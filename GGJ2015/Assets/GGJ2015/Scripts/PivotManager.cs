using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.GGJ2015.Scripts {
    public static class PivotManager {

        private static List<Pivot> _pivots;


        static PivotManager() {
            CreatePivots();
        }


        private static void CreatePivots() {
            _pivots = new List<Pivot> {
                new Pivot {
                    Id = PivotIds.Flower,
                    Description = "There is a flower.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Water it.",
                            NextPivot = PivotIds.Puddle
                        },
                        new Choice {
                            Description = "Stomp on it.",
                            NextPivot = PivotIds.FlowerSprout
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.Puddle,
                    Description = "You overwatered the poor thing. There's only a puddle now.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Pee in it.",
                            NextPivot = PivotIds.PeePuddle
                        },
                        new Choice {
                            Description = "Drink it.",
                            NextPivot = PivotIds.RumblyTummy
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.PeePuddle,
                    Description = "Now the puddle is yellow.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Drink it.",
                            NextPivot = PivotIds.RumblyTummy
                        },
                        new Choice {
                            Description = "Share it with a friend.",
                            NextPivot = PivotIds.FriendDies
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.YouDied,
                    Description = "You died.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Be buried.",
                            NextPivot = PivotIds.None //TODO
                        },
                        new Choice {
                            Description = "Be cremated.",
                            NextPivot = PivotIds.None //TODO
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.RumblyTummy,
                    Description = "You feel a rumbly in your tummy.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Go to the hospital.",
                            NextPivot = PivotIds.GotEbola
                        },
                        new Choice {
                            Description = "Go home and sleep it off.",
                            NextPivot = PivotIds.AreYouSure
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.GotEbola,
                    Description = "You were diagnosed with Ebola.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Infect others.",
                            NextPivot = PivotIds.None //TODO
                        },
                        new Choice {
                            Description = "Do the right thing.",
                            NextPivot = PivotIds.YouDied
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.AreYouSure,
                    Description = "Are you sure?",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Yes.",
                            NextPivot = PivotIds.YouDied
                        },
                        new Choice {
                            Description = "Go to the hospital.",
                            NextPivot = PivotIds.GotEbola
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.FriendDies,
                    Description = "Your friend dies.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Tell the truth.",
                            NextPivot = PivotIds.InTrial,
                        },
                        new Choice {
                            Description = "Hide the body.",
                            NextPivot = PivotIds.Grave
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.InTrial,
                    Description = "You were arrested and put on trial.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Plead insanity.",
                            NextPivot = PivotIds.None //TODO
                        },
                        new Choice {
                            Description = "Settle out of court.",
                            NextPivot = PivotIds.None //TODO
                        }
                    }
                },
                new Pivot {
                    //TODO
                    Id = PivotIds.FoundInnocent,
                    Description = "The jury found you innocent.",
                    Choices = new List<Choice> { },
                },
                new Pivot {
                    //TODO
                    Id = PivotIds.Grave,
                    Description = "A flower sprouts from the grave.",
                    Choices = new List<Choice> { },
                },
                new Pivot {
                    Id = PivotIds.FlowerMutates,
                    Description = "The flower mutates into a talking Venus Flytrap.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Be friends.",
                            NextPivot = PivotIds.None //TODO
                        },
                        new Choice {
                            Description = "Be lovers.",
                            NextPivot = PivotIds.MakeFlowerBaby //TODO
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.BeeUtopia,
                    Description = "The flowers have become a utopia for bees.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Dance with the bees.",
                            NextPivot = PivotIds.GetStung
                        },
                        new Choice {
                            Description = "Experiment on the bees.",
                            NextPivot = PivotIds.Experiment
                        }
                    }
                },
                new Pivot {
                    //TODO
                    Id = PivotIds.MakeFlowerBaby,
                    Description = "Make a flower baby.",
                    Choices = new List<Choice> { },
                },
                new Pivot {
                    //TODO
                    Id = PivotIds.GetStung,
                    Description = "You got stung.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Go to the hospital.",
                            NextPivot = PivotIds.GotEbola
                        },
                        new Choice {
                            Description = "Go home and sleep it off.",
                            NextPivot = PivotIds.AreYouSure
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.Experiment,
                    Description = "Genetic splicing makes the bees human-sized.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "The bees go to infinity and beyond.",
                            NextPivot = PivotIds.None //TODO
                        },
                        new Choice {
                            Description = "The bees stay on Earth.",
                            NextPivot = PivotIds.None //TODO
                        },
                    }
                }
            };
        }
    }
}

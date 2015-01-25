using System;
using System.Collections.Generic;
using Assets.GGJ2015.Scripts.Audio;
using Assets.GGJ2015.Scripts.PropertyAttributes;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Pivots {
    public class Story {

        private readonly Dictionary<string, Pivot> _idsToPivots = new Dictionary<string, Pivot>();
        [SerializeField, Readonly] public Pivot Root; 


        public Story() {
            var pivots = GeneratePivots();
            foreach (var pivot in pivots) {
                if (pivot.Id == PivotIds.StoryRoot) {
                    Root = pivot;
                }
                if (_idsToPivots.ContainsKey(pivot.Id)) {
                    Debug.LogWarning(string.Format("Pivot '{0}' contains duplicate id '{1}'!", pivot.Description, pivot.Id));
                }
                _idsToPivots[pivot.Id] = pivot;
            }
        }


        public Pivot GetPivot(string id) {
            if (!_idsToPivots.ContainsKey(id)) {
                throw new ArgumentException("There is no pivot with id!");
            }
            return _idsToPivots[id];
        }


        private IEnumerable<Pivot> GeneratePivots() {
            return new List<Pivot> {
                new Pivot {
                    Id = PivotIds.StoryRoot,
                    Description = "There is a flower.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Water it.",
                            NextPivot = PivotIds.Puddle,
                            OnTriggerTrackName = AudioClips.BgNormal
                        },
                        new Choice {
                            Description = "Stomp on it.",
                            NextPivot = PivotIds.FlowerSprout,
                            OnTriggerTrackName = AudioClips.BgNormal
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.Puddle,
                    Description = "You overwatered the poor thing. There's only a puddle now.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Pee in it.",
                            NextPivot = PivotIds.PeePuddle,
							OnTriggerTrackName = AudioClips.BgPeePuddle
                        },
                        new Choice {
                            Description = "Drink it.",
                            NextPivot = PivotIds.RumblyTummy,
							OnTriggerTrackName = AudioClips.BgRumblyTummy
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.PeePuddle,
                    Description = "Now the puddle is yellow.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Drink it.",
                            NextPivot = PivotIds.RumblyTummy,
							OnTriggerTrackName = AudioClips.BgRumblyTummy
                        },
                        new Choice {
                            Description = "Share it with a friend.",
                            NextPivot = PivotIds.FriendDies,
							OnTriggerTrackName = AudioClips.BgFriendDies
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.RumblyTummy,
                    Description = "You feel a rumbly in your tummy.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Go to the hospital.",
                            NextPivot = PivotIds.GotEbola,
							OnTriggerTrackName = AudioClips.BgGotEbola
                        },
                        new Choice {
                            Description = "Go home and sleep it off.",
							NextPivot = PivotIds.Grave,
							OnTriggerTrackName = AudioClips.BgGrave
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.GotEbola,
                    Description = "You were diagnosed with Ebola.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Infect others.",
                            NextPivot = PivotIds.TerriblePerson,
							OnTriggerTrackName = AudioClips.BgTerriblePerson
                        },
                        new Choice {
                            Description = "Do the right thing.",
                            NextPivot = PivotIds.Grave,
							OnTriggerTrackName = AudioClips.BgGrave
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.FriendDies,
                    Description = "Your friend dies.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Get a new friend",
                            NextPivot = PivotIds.StoryRoot,
							OnTriggerTrackName = AudioClips.BgNormal
                        },
                        new Choice {
                            Description = "Hide the body.",
                            NextPivot = PivotIds.Grave
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.Grave,
                    Description = "A flower sprouts from the grave.",
					Choices = new List<Choice> {
						new Choice {
							Description = "Water the flower.",
							NextPivot = PivotIds.Puddle
						},
						new Choice {
							Description = "Stomp on it.",
							NextPivot = PivotIds.FlowerSprout
						}
					}
                },
                new Pivot {
                    Id = PivotIds.FlowerMutates,
                    Description = "The flower mutates into a talking Venus Flytrap.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Be friends.",
                            NextPivot = PivotIds.Grave
                        },
                        new Choice {
                            Description = "Be lovers & get busy.",
                            NextPivot = PivotIds.BeeUtopia
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.BeeUtopia,
                    Description = "The flowers have become a utopia for bees.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Dance with the bees.",
                            NextPivot = PivotIds.GotEbola
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
                    Id = PivotIds.GetStung,
                    Description = "You got stung.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Go to the hospital.",
                            NextPivot = PivotIds.GotEbola
                        },
                        new Choice {
                            Description = "Go home and sleep it off.",
                            NextPivot = PivotIds.Grave
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.Experiment,
                    Description = "Genetic splicing makes the bees human-sized.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "The bees go to infinity and beyond.",
                            NextPivot = PivotIds.StoryRoot
                        },
                        new Choice {
                            Description = "Chill.",
                            NextPivot = PivotIds.GetStung
                        },
                    }
                },
				new Pivot {
					Id = PivotIds.TerriblePerson,
					Description = "Given a flower seed by the Voice to plant in honor of your friend. Plant it?",
					Choices = new List<Choice> {
						new Choice {
							Description = "Sure, punish me",
							NextPivot = PivotIds.GetStung
						},
						new Choice {
							Description = "Fry Me",
							NextPivot = PivotIds.Grave
						},
					}
				},
				new Pivot {
					Id = PivotIds.FlowerSprout,
					Description = "What are you gonna do now?",
					Choices = new List<Choice> {
						new Choice {
							Description = "Pesticide anyone?",
							NextPivot = PivotIds.FlowerMutates
						},
						new Choice {
							Description = "Let them grow.",
							NextPivot = PivotIds.BeeUtopia
						},
					}
				},
            };



        }
    }
}

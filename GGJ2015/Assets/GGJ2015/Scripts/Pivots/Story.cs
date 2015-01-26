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
                            OnTriggerAnimationName = Animations.WaterFlower,
                            OnTriggerTrackName = AudioClips.BgNormal
                        },
                        new Choice {
                            Description = "Stomp on it.",
                            NextPivot = PivotIds.FlowerSprout,
                            OnTriggerAnimationName = Animations.BootStomp,
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
                            OnTriggerAnimationName = Animations.Pee,
							OnTriggerTrackName = AudioClips.BgPeePuddle
                        },
                        new Choice {
                            Description = "Drink it.",
                            NextPivot = PivotIds.RumblyTummy,
                            OnTriggerAnimationName = Animations.DrinkWater,
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
                            OnTriggerAnimationName = Animations.DrinkPee,
							OnTriggerTrackName = AudioClips.BgRumblyTummy
                        },
                        new Choice {
                            Description = "Share it with a friend.",
                            NextPivot = PivotIds.FriendDies,
                            OnTriggerAnimationName = Animations.DrinkPee,
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
                            OnTriggerAnimationName = Animations.GotEbola,
							OnTriggerTrackName = AudioClips.BgGotEbola
                        },
                        new Choice {
                            Description = "Go home and sleep it off.",
							NextPivot = PivotIds.StoryRoot,
                            OnTriggerAnimationName = Animations.Grave,
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
                            OnTriggerAnimationName = Animations.Grave, //TODO
							OnTriggerTrackName = AudioClips.BgTerriblePerson
                        },
                        new Choice {
                            Description = "Do the right thing.",
                            NextPivot = PivotIds.StoryRoot,
                            OnTriggerAnimationName = Animations.Grave,
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
                            OnTriggerAnimationName = Animations.Grave,
							OnTriggerTrackName = AudioClips.BgNormal
                        },
                        new Choice {
                            Description = "Hide the body.",
                            OnTriggerAnimationName = Animations.Grave,
                            NextPivot = PivotIds.StoryRoot,
							OnTriggerTrackName = AudioClips.BgGrave
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.FlowerMutates,
                    Description = "The flower mutates into a talking Venus Flytrap.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Be friends.",
                            NextPivot = PivotIds.StoryRoot,
                            OnTriggerAnimationName = Animations.Grave,
							OnTriggerTrackName = AudioClips.BgGrave
                        },
                        new Choice {
                            Description = "Be lovers & get busy.",
                            NextPivot = PivotIds.BeeUtopia,
                            OnTriggerAnimationName = Animations.BeLovers,
							OnTriggerTrackName = AudioClips.BgBeLovers
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.BeeUtopia,
                    Description = "The flowers have become a utopia for bees.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Dance with the bees.",
                            NextPivot = PivotIds.GotEbola,
                            OnTriggerAnimationName = Animations.GetStung,
							OnTriggerTrackName = AudioClips.BgGotEbola
                        },
                        new Choice {
                            Description = "Experiment on the bees.",
                            NextPivot = PivotIds.Experiment,
                            OnTriggerAnimationName = Animations.BeeExperiment,
							OnTriggerTrackName = AudioClips.BgExperiment
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.GetStung,
                    Description = "You got stung.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "Go to the hospital.",
                            NextPivot = PivotIds.GotEbola,
                            OnTriggerAnimationName = Animations.GotEbola,
							OnTriggerTrackName = AudioClips.BgGotEbola

                        },
                        new Choice {
                            Description = "Go home and sleep it off.",
                            NextPivot = PivotIds.StoryRoot,
                            OnTriggerAnimationName = Animations.Grave,
							OnTriggerTrackName = AudioClips.BgNormal
                        }
                    }
                },
                new Pivot {
                    Id = PivotIds.Experiment,
                    Description = "Genetic splicing makes the bees human-sized.",
                    Choices = new List<Choice> {
                        new Choice {
                            Description = "The bees go to infinity and beyond.",
                            NextPivot = PivotIds.StoryRoot,
                            OnTriggerAnimationName = Animations.SpaceBees,
							OnTriggerTrackName = AudioClips.BgNormal
                        },
                        new Choice {
                            Description = "Chill.",
                            NextPivot = PivotIds.GetStung,
                            OnTriggerAnimationName = Animations.GetStung,
							OnTriggerTrackName = AudioClips.BgGetStung
                        },
                    }
                },
				new Pivot {
					Id = PivotIds.TerriblePerson,
					Description = "Given a flower seed by the Voice to plant in honor of your friend. Plant it?",
					Choices = new List<Choice> {
						new Choice {
							Description = "Sure, punish me",
							NextPivot = PivotIds.GetStung,
                            OnTriggerAnimationName = Animations.GetStung,
							OnTriggerTrackName = AudioClips.BgGetStung
						},
						new Choice {
							Description = "Fry Me",
							NextPivot = PivotIds.StoryRoot,
                            OnTriggerAnimationName = Animations.Grave, //TODO: WItch
							OnTriggerTrackName = AudioClips.BgNormal
						},
					}
				},
				new Pivot {
					Id = PivotIds.FlowerSprout,
					Description = "What are you gonna do now?",
					Choices = new List<Choice> {
						new Choice {
							Description = "Pesticide anyone?",
							NextPivot = PivotIds.FlowerMutates,
                            OnTriggerAnimationName = Animations.Pesticide,
							OnTriggerTrackName = AudioClips.BgFlowerMutates
						},
						new Choice {
							Description = "Let them grow.",
							NextPivot = PivotIds.BeeUtopia,
                            OnTriggerAnimationName = Animations.FlowersSprout,
							OnTriggerTrackName = AudioClips.BgBeeUtopia
						},
					}
				},
            };



        }
    }
}

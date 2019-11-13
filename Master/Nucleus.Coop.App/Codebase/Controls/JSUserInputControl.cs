using Newtonsoft.Json.Linq;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Handler;
using Nucleus.Gaming.Platform.Windows.Controls;
using Nucleus.Gaming.Windows.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Gaming.App.Controls {
    public class JSUserInputControl : UserInputControl {
        public override bool CanProceed {
            get { return canProceed; }
        }
        public override string Title {
            get { return CustomStep.Title; }
        }
        public override bool CanPlay {
            get { return canPlay; }
        }
        public HandlerDataManager DataManager {
            get { return dataManager; }
            set { dataManager = value; }
        }

        // TODO: why is this a field
        public CustomStep CustomStep;

        private bool canProceed;
        private bool canPlay;

        private Font nameFont;
        private Font detailsFont;
        private IList collection;
        private HandlerDataManager dataManager;

        public JSUserInputControl() {
            nameFont = new Font("Segoe UI", 24);
            detailsFont = new Font("Segoe UI", 18);
        }

        public bool HasProperty(IDictionary<string, Object> expando, string key) {
            return expando.ContainsKey(key);
        }

        public override void Initialize(HandlerData handlerData, UserGameInfo game, GameProfile profile) {
            base.Initialize(handlerData, game, profile);

            Controls.Clear();

            // grab the CustomStep and extract what we have to show from it
            GameOption option = CustomStep.Option;

            if (option.IsCollection()) {
                ControlListBox list = new ControlListBox();
                list.Size = this.Size;
                list.AutoScroll = true;
                list.SelectedChanged += List_SelectedChanged;

                Controls.Add(list);

                collection = option.GetCollection();
                for (int i = 0; i < collection.Count; i++) {
                    object val = collection[i];

                    // TODO: make image options
                    if (!(val is IDictionary<string, JToken>)) {
                        continue;
                    }

                    CoolListControl control = new CoolListControl(true);
                    control.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                    control.BackColor = Color.FromArgb(54, 57, 63);

                    control.Size = new Size(list.Width, 120);
                    control.Data = val;
                    control.OnSelected += Control_OnSelected;

                    IDictionary<string, JToken> value = (IDictionary<string, JToken>)val;
                    string name = value["Name"].ToString();

                    control.Title = name;
                    control.TitleFont = nameFont;
                    control.DetailsFont = detailsFont;

                    string details = "";
                    JToken detailsObj;
                    if (value.TryGetValue("Details", out detailsObj)) {
                        details = detailsObj.ToString();
                        control.Details = details;
                    }

                    JToken imageUrlObj;
                    value.TryGetValue("ImageUrl", out imageUrlObj);
                    if (imageUrlObj != null) {
                        string imageUrl = imageUrlObj.ToString();
                        if (!string.IsNullOrEmpty(imageUrl)) {
                            Image img = DataManager.Content.LoadImage(imageUrl);

                            PictureBox box = new PictureBox();
                            box.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                            box.Size = new Size(140, 80);
                            box.Location = new Point(list.Width - box.Width - 10, 20);
                            box.SizeMode = PictureBoxSizeMode.Zoom;
                            box.Image = img;
                            control.Controls.Add(box);
                        }
                    }

                    list.Controls.Add(control);
                }
            } else {
            }
        }

        private void List_SelectedChanged(Control selected) {
            if (selected is CoolListControl) {
                CoolListControl list = (CoolListControl)selected;
                profile.Options[CustomStep.Option.Key] = list.Data;

                canProceed = true;
                CanPlayUpdated(true, true);
            }
        }

        private void Control_OnSelected(object obj) {
        }
    }
}

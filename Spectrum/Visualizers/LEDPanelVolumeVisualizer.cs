﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectrum.Base;
using Spectrum.LEDs;
using Spectrum.Audio;

namespace Spectrum {

  class LEDPanelVolumeVisualizer : Visualizer {

    private Configuration config;
    private AudioInput audio;
    private CartesianTeensyOutput teensy;

    public LEDPanelVolumeVisualizer(
      Configuration config,
      AudioInput audio,
      CartesianTeensyOutput teensy
    ) {
      this.config = config;
      this.audio = audio;
      this.teensy = teensy;
      this.teensy.RegisterVisualizer(this);
    }

    public int Priority {
      get {
        return 1;
      }
    }

    // We don't actually care about this
    public bool Enabled { get; set; } = false;

    public Input[] GetInputs() {
      return new Input[] { this.audio };
    }

    public void Visualize() {
      int numColumnsToLight = (int)(this.audio.Volume * 30);
      for (int j = 0; j < 40; j++) {
        for (int i = 0; i < 30; i++) {
          int color = numColumnsToLight > i ? 0x111111 : 0x000000;
          this.teensy.SetPixel(i, j, color);
        }
      }
      this.teensy.Flush();
    }

  }

}
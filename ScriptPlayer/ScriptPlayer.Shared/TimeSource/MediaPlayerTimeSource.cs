﻿using System;
using System.Windows.Media;

namespace ScriptPlayer.Shared
{
    public class MediaPlayerTimeSource : TimeSource, IDisposable
    {
        private readonly MediaPlayer _player;
        private readonly ISampleClock _clock;

        public MediaPlayerTimeSource(MediaPlayer player, ISampleClock clock)
        {
            _player = player;
            _player.MediaOpened += PlayerOnMediaOpened;
            _player.MediaEnded += PlayerOnMediaEnded;

            _clock = clock;
            _clock.Tick += ClockOnTick;
        }

        private void PlayerOnMediaEnded(object sender, EventArgs eventArgs)
        {
            IsPlaying = false;
        }

        private void PlayerOnMediaOpened(object sender, EventArgs eventArgs)
        {
            if(_player.NaturalDuration.HasTimeSpan)
                Duration = _player.NaturalDuration.TimeSpan;

            _player.Play();
        }

        private void ClockOnTick(object sender, EventArgs eventArgs)
        {
            Progress = _player.Position;
        }
        
        public override void Play()
        {
            if (IsPlaying)
                return;

             IsPlaying = true;
            _player.Play();
        }

        public override void Pause()
        {
            if (!IsPlaying)
                return;

            IsPlaying = false;
            _player.Pause();
        }

        public override void SetPosition(TimeSpan position)
        {
            _player.Position = position;
        }

        public void Dispose()
        {
            _clock.Tick -= ClockOnTick;
            _player.Stop();
            _player.Close();
        }

        public override bool CanPlayPause => true;
        public override bool CanSeek => true;
        public override bool CanOpenMedia => true;
    }
}

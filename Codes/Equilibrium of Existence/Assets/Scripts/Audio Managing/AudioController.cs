using System.Collections;
using System.Collections.Generic;
using UnityEngine;





        public class AudioController : MonoBehaviour
{
        // members
        public static AudioController instance;
        


        public AudioTrack[] tracks;
        
        private Hashtable m_AudioTable; // relationship between audio names (key) and audio tracks (value)
        private Hashtable m_JobTable;  // relationship between  audio names (key) and jobs (value) (Coroutine, IEnumerator)

        
        [System.Serializable]
        public class AudioObject
        {
            public AudioNames.AudioName name;
            public AudioClip clip;
        }

        [System.Serializable]
        
        //Hangi ses hangi source ile çalacak bilgisinin belirlendiği sınıftır.
        public class AudioTrack
        {
            public AudioSource source;
            public AudioObject[] audio;
        }

        private class AudioJob
        {
            public readonly AudioAction action;
            public AudioNames.AudioName name;
            public bool fade;
            public WaitForSeconds delay;
            
            public AudioJob(AudioAction _action, AudioNames.AudioName _name, bool _fade,float _delay)
            {
                action = _action;
                name = _name;
                fade = _fade;
                delay = _delay > 0f ? new WaitForSeconds(_delay) : null;
            }
        }

        private enum AudioAction
        {
            START,
            STOP,
            RESTART,
            PAUSE
        }


        #region Unity Functions
 private void Awake()
        {
            if (!instance)
            {   
                Configure();
            }
        }

        private void OnDisable()
        {
            Dispose();
        }

#if UNITY_EDITOR

      

     


#endif
       
        #endregion

        #region Public Functions
        public void PlayAudio(AudioNames.AudioName _name, bool _fade=false,float _delay=0.0f)
        {
            AddJob(new AudioJob(AudioAction.START, _name, _fade, _delay));
        }
        public void StopAudio(AudioNames.AudioName _name, bool _fade = false, float _delay = 0.0f)
        {
            AddJob(new AudioJob(AudioAction.STOP, _name, _fade, _delay));
        }

        public void RestartAudio(AudioNames.AudioName _name, bool _fade = false, float _delay = 0.0f)
        {
            AddJob(new AudioJob(AudioAction.RESTART, _name, _fade, _delay));
        }

        public void PauseAudio(AudioNames.AudioName _name, bool _fade = false, float _delay = 0.0f)
        {
        AddJob(new AudioJob(AudioAction.PAUSE, _name, _fade, _delay));
        }
        #endregion

        #region Private Functions

        private void Dispose()
        {
            foreach(DictionaryEntry _entry in m_JobTable)
            {
                IEnumerator _job = (IEnumerator)_entry.Value;
                StopCoroutine(_job);
            }
        }
        private void Configure()
        {
            instance = this;
            m_AudioTable = new Hashtable();
            m_JobTable = new Hashtable();
            GenerateAudioTable();
        }

        private void GenerateAudioTable()
        {
            foreach(AudioTrack _track in tracks)
            {
                foreach(AudioObject _obj in _track.audio)
                {
                    // do not duplicate keys
                    if (m_AudioTable.ContainsKey(_obj.name))
                    {
                        Debug.Log("You are trying to register audio [" + _obj.name + "] that has already been registered.");
                    } else
                    {
                        m_AudioTable.Add(_obj.name, _track);
                        Debug.Log("Registering audio [" + _obj.name + "]");
                    }
                }
            }
        }

        private IEnumerator RunAudioJob(AudioJob _job)
        {
            if (_job.delay != null) yield return _job.delay;

            AudioTrack _track = (AudioTrack)m_AudioTable[_job.name];
            _track.source.clip = GetAudioClipFromAudioTrack(_job.name, _track);

            float _initial = _job.action == AudioAction.START || _job.action == AudioAction.RESTART ? 0.0f : 1.0f;
            float _target = _initial == 0 ? 1 : 0;

            switch (_job.action)
            {
                case AudioAction.START:
                    _track.source.Play();

                    break;

                case AudioAction.STOP:
                   if (!_job.fade)
                    {
                        _track.source.Stop();
                    }
                    
                    break;

                case AudioAction.RESTART:
                    _track.source.Stop();
                    _track.source.Play();
                    break;

                case AudioAction.PAUSE:
                    _track.source.Pause();
                    break;
                    
            }

            if (_job.fade)
            {
               
                float _duration = 1.0f;
                float _timer = 0.0f;

                while (_timer <= _duration)
                {
                    _track.source.volume = Mathf.Lerp(_initial, _target, _timer / _duration);
                    _timer += Time.deltaTime;
                    yield return null;
                }

                if(_job.action == AudioAction.STOP)
                {
                    _track.source.Stop();
                }
            }

            m_JobTable.Remove(_job.name);
            Debug.Log("Job Count: " + m_JobTable.Count);
            
            yield return null;

        }

        private void AddJob(AudioJob _job)
        {
            // remove conflicted jobs
            RemoveConflictingJobs(_job.name);

            // start job
            IEnumerator _jobRunner = RunAudioJob(_job);
            m_JobTable.Add(_job.name, _jobRunner);
            StartCoroutine(_jobRunner);
            Debug.Log("[" + _job.name + "] with operation: " + _job.action);
        }

        private void RemoveJob(AudioNames.AudioName _name)
        {
            if (!m_JobTable.ContainsKey(_name))
            {
                Debug.Log("You are trying to remove a job that isn't running");
                return;
            }

            IEnumerator _runningJob = (IEnumerator)m_JobTable[_name];
            StopCoroutine(_runningJob);
            m_JobTable.Remove(_name);
        }
        
        private void RemoveConflictingJobs(AudioNames.AudioName _name)
        {
            if (m_JobTable.ContainsKey(_name))
            {
                RemoveJob(_name);
            }

            AudioNames.AudioName _conflictAudio = AudioNames.AudioName.None;
            foreach(DictionaryEntry _entry in m_JobTable)
            {
                AudioNames.AudioName _audioName = (AudioNames.AudioName)_entry.Key;
                AudioTrack _audioTrackInUse = (AudioTrack)m_AudioTable[_audioName];
                AudioTrack _audioTrackNeeded = (AudioTrack)m_AudioTable[_name];
                if (_audioTrackNeeded.source == _audioTrackInUse.source)
                {
                    _conflictAudio = _audioName;
                }
            }
            if (_conflictAudio!= AudioNames.AudioName.None)
            {
                RemoveJob(_conflictAudio);
            }

            
        }
        public AudioClip GetAudioClipFromAudioTrack(AudioNames.AudioName _name, AudioTrack _track)
        {
            foreach(AudioObject _obj in _track.audio)
            {
                if (_obj.name == _name)
                {
                    return _obj.clip;
                }
            }
            return null;
        }
        
        #endregion


















    }


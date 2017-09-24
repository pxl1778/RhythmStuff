using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum BEAT
{
    RIGHT, UP, DOWN, LEFT, CIRCLE, TRIANGLE, SQUARE, X
}

public class Staff : ScriptableObject {
    private ArrayList beats = new ArrayList();
    private Dictionary<BEAT, AudioClip> clips = new Dictionary<BEAT, AudioClip>();
    private string[] attacks = new string[] { "CIRCLECIRCLECIRCLECIRCLE" };
    private Player player;

    public Staff(Player pPlayer)
    {
        player = pPlayer;
        clips.Add(BEAT.UP,(AudioClip)AssetDatabase.LoadAssetAtPath("Assets/sounds/BigAsteroids/Big Asteroid1.mp3", typeof(AudioClip)));
        clips.Add(BEAT.DOWN, (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/sounds/BigAsteroids/Big Asteroid2.mp3", typeof(AudioClip)));
        clips.Add(BEAT.LEFT, (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/sounds/BigAsteroids/Big Asteroid3.mp3", typeof(AudioClip)));
        clips.Add(BEAT.RIGHT, (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/sounds/BigAsteroids/Big Asteroid4.mp3", typeof(AudioClip)));
        clips.Add(BEAT.CIRCLE, (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/sounds/LittleAsteroids/Little asteroids1.mp3", typeof(AudioClip)));
        clips.Add(BEAT.TRIANGLE, (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/sounds/LittleAsteroids/Little asteroids7.mp3", typeof(AudioClip)));
        clips.Add(BEAT.SQUARE, (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/sounds/LittleAsteroids/Little asteroids3.mp3", typeof(AudioClip)));
        clips.Add(BEAT.X, (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/sounds/LittleAsteroids/Little asteroids4.mp3", typeof(AudioClip)));
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addBeat(BEAT beat)
    {
        AudioSource.PlayClipAtPoint(clips[beat], player.transform.position);
        Debug.Log(beat);
        if(beat < BEAT.CIRCLE || beats.Count >= 4)
        {
            beats.Clear();
        }
        else
        {
            var beatString = "";
            beats.Add(beat);
            if(beats.Count == 4)
            {
                //creating beat string out of the 4 beats pressed
                foreach(BEAT b in beats)
                {
                    beatString += b;
                }
                //comparing with all known attacks to see if it will work
                foreach (string s in attacks)
                {
                    if (s == beatString)
                    {
                        Debug.Log("attack");
                        beats.Clear();
                    }
                }
                //if there wasn't an attack, get rid of the 
                if(beats.Count > 0)
                {
                    beats.RemoveAt(0);
                }
            }
        }
    }
}

namespace WebAudio

open WebSharper.InterfaceGenerator

module Definition =
    open WebSharper
    open WebSharper.JavaScript.Dom

    let O = T<unit>
    let Error = T<exn>
    let Ulong = T<int>
    let Event = T<Event>

    let AudioNode = Type.New ()
    let AudioDestinationNode = Type.New () 
    let PannerNode = Type.New ()
    let MediaElementAudioSourceNode = Type.New () 
    let ConvolverNode = Type.New () 
    let AnalyserNode = Type.New ()
    let ChannelSplitterNode = Type.New ()
    let ChannelMergerNode = Type.New ()
    let DynamicsCompressorNode = Type.New ()
    let BiquadFilterNode = Type.New ()
    let WaveShaperNode = Type.New ()
    let OscillatorNode = Type.New ()
    let MediaStreamAudioSourceNode = Type.New ()
    let MediaStreamAudioDestinationNode = Type.New () 
    let AudioBuffer = Type.New ()

    let OfflineAudioCompletitionEvent = 
        Class "OfflineAudioCompletitionEvent"
        |=> Inherits Event
        |+> Instance [
            "renderedBuffer" =? AudioBuffer
        ]

    let AudioDestinationNodeClass =
        Class "AudioDestinationNode"
        |=> Inherits AudioNode
        |=> AudioDestinationNode
        |+> Instance [
            "maxChannelCount" =? Ulong
        ]

    let AudioListener = 
        Class "AudioListener"
        |+> Instance [
            "dopplerFactor" =@ T<double>
            |> WithComment "A constant used to determine the amount of pitch shift to use when rendering a doppler effect."
            "speedOfSound" =@ T<double>
            |> WithComment "The speed of sound used for calculating doppler shift."

            "setPosition" => (T<double>?x * T<double>?y * T<double>?z) ^-> O
            |> WithComment "Sets the position of the listener in a 3D cartesian coordinate space. PannerNode objects use this position relative to individual audio sources for spatialization."
            "setOrientation" => (T<double>?x * T<double>?y * T<double>?z * T<double>?xUp * T<double>?yUp * T<double>?zUp) ^-> O
            |> WithComment "Describes which direction the listener is pointing in the 3D cartesian coordinate space. Both a front vector and an up vector are provide."
            //"setVelocity" => (T<double>?x * T<double>?y * T<double>?z) ^-> O
        ]

    let AudioBufferClass = 
        Class "AudioBuffer"
        |=> AudioBuffer
        |+> Instance [
            "sampleRate" =? T<float>
            |> WithComment "The sample-rate for the PCM audio data in samples per second."
            "length" =? T<int>
            |> WithComment "Length of the PCM audio data in sample-frames."
            "duration" =? T<double>
            |> WithComment "Duration of the PCM audio data in seconds."
            "numberOfChannels" =? T<int>
            |> WithComment "The number of discrete audio channels."

            "getChannelData" => Ulong?channel ^-> T<JavaScript.Float32Array>
            |> WithComment "Returns the Float32Array representing the PCM audio data for the specific channel."

            "copyFromChannel" => (T<JavaScript.Float32Array>?destination * T<int>?channelNumber * !? Ulong?startInChannel) ^-> O
            |> WithComment "Copies the samples from the specified channel of the AudioBuffer to the destination array."
            "copyToChannel" => (T<JavaScript.Float32Array>?source * T<int>?channelNumber * !? Ulong?startInChannel) ^-> O
            |> WithComment "Copies the samples to the specified channel of the , from the source array."
        ]

    let AudioParam =
        Class "AudioParam"
        |+> Instance [
            "value" =@ T<float>
            |> WithComment "The parameter's floating-point value."
            "defaultValue" =? T<float>
            |> WithComment "Initial value for the value attribute."

            "setValueAtTime" => (T<float>?value * T<double>?startTime) ^-> O
            |> WithComment "Schedules a parameter value change at the given time."
            "linearRampToValueAtTime" => (T<float>?value * T<double>?endTime) ^-> O
            |> WithComment "Schedules a linear continuous change in parameter value from the previous scheduled parameter value to the given value."
            "exponentialRampToValueAtTime" => (T<float>?value * T<double>?endTime) ^-> O
            |> WithComment "Schedules an exponential continuous change in parameter value from the previous scheduled parameter value to the given value."

            "setTargetAtTime" => (T<float>?target * T<double>?startTime * T<double>?timeConstant) ^-> O
            |> WithComment "Start exponentially approaching the target value at the given time with a rate having the given time constant."
            "setValueCurveAtTime" => (T<JavaScript.Float32Array>?values * T<double>?startTime * T<double>?duration) ^-> O
            |> WithComment "Sets an array of arbitrary parameter values starting at the given time for the given duration."
            "cancelScheduledValues" => T<double>?startTime ^-> O
            |> WithComment "Cancels all scheduled parameter changes with times greater than or equal to startTime."
        ]

    let AudioBufferSourceNode = 
        Class "AudioBufferSourceNode"
        |=> Inherits AudioNode
        |+> Instance [
            "buffer" =@ AudioBuffer
            |> WithComment "Represents the audio asset to be played."
            "playbackRate" =? AudioParam
            |> WithComment "The speed at which to render the audio stream."

            "loop" =@ T<bool>
            |> WithComment "Indicates if the audio data should play in a loop. The default value is false."
            "loopStart" =@ T<double>
            |> WithComment "An optional value in seconds where looping should begin if the loop attribute is true."
            "loopEnd" =@ T<double>
            |> WithComment "An optional value in seconds where looping should end if the loop attribute is true."

            "start" => (!? T<double>?``when`` * !? T<double>?offset * !? T<double>?duration) ^-> O
            |> WithComment "Schedules a sound to playback at an exact time."
            "stop" => (!? T<double>?``when``) ^-> O
            |> WithComment "Schedules a sound to stop playback at an exact time."

            "onended" =@ Event?event ^-> O
        ]

    let GainNode = 
        Class "GainNode"
        |=> Inherits AudioNode
        |+> Instance [
            "gain" =? AudioParam
            |> WithComment "Represents the amount of gain to apply. "
        ]

    let DelayNode = 
        Class "DelayNode"
        |=> Inherits AudioNode
        |+> Instance [
            "delayTime" =? AudioParam
            |> WithComment "Represents the amount of delay in seconds to apply"
        ]

    let PeriodicWave = 
        Class "PeriodicWave"

    let ChannelCountMode =
        Pattern.EnumStrings "ChannelountMode" [
            "max"
            "clamped-max"
            "explicit"
        ]
    
    let ChannelInterpretation =
        Pattern.EnumStrings "ChannelInterpretation" [
            "speakers"
            "discrete"
        ]

    let  AudioProcessingEvent =
        Class "AudioProcessingEvent" 
        |=> Inherits Event
        |+> Instance [
            "playbackTime" =? T<double>
            |> WithComment "The time when the audio will be played in the same time coordinate system as the AudioContext's currentTime."
            "inputBuffer" =? AudioBuffer
            |> WithComment "An AudioBuffer containing the input audio data."
            "outputBuffer" =? AudioBuffer
            |> WithComment "An AudioBuffer where the output audio data should be written."
        ]

    let ScriptProcessorNode = 
        Class "ScriptProcessorNode"
        |=> Inherits AudioNode
        |+> Instance [
            "onaudioprocess" =@ AudioProcessingEvent ^-> O 

            "bufferSize" =? T<int>
            |> WithComment "The size of the buffer (in sample-frames) which needs to be processed each time onaudioprocess is called."
        ]

    let AudioContext = 
        let DecodesuccessCallback = AudioBuffer ^-> O 
        let ErrorCallback = O ^-> O

        Class "AudioContext"
        |=> Inherits T<EventTarget>
        |+> Static [
            Constructor O 
            |> WithInline "new (window.AudioContext || window.webkitAudioContext)()"
        ]
        |+> Instance [
            "destination" =? AudioDestinationNode
            |> WithComment "An AudioDestinationNode with a single input representing the final destination for all audio."
            "sampleRate" =? T<float>
            |> WithComment "The sample rate (in sample-frames per second) at which the AudioContext handles audio."
            "currentTime" =? T<double>
            |> WithComment "This is a time in seconds which starts at zero when the context is created and increases in real-time."
            "listener" =? AudioListener
            |> WithComment "An AudioListener which is used for 3D spatialization."
            
            "createBuffer" => (Ulong?numberOfChannels * Ulong?length * T<float>?sampleRate) ^-> AudioBuffer
            |> WithComment "Creates an AudioBuffer of the given size. The audio data in the buffer will be zero-initialized (silent).\
                            An NOT_SUPPORTED_ERR exception is thrown if any of the arguments is negative, zero, or outside its nominal range."
            "decodeAudioData" => (T<JavaScript.ArrayBuffer>?audioData * DecodesuccessCallback?successCallback * !? ErrorCallback?errorCallback) ^-> O
            |> WithComment "Asynchronously decodes the audio file data contained in the ArrayBuffer."
            //"decodeAudioData" => (T<JavaScript.ArrayBuffer>?audioData * !? DecodesuccessCallback?successCallback * !? ErrorCallback?errorCallback) ^-> Promise<AudioBuffer>

            "createBufferSource" => O ^-> AudioBufferSourceNode
            "createMediaElementSource" => T<JavaScript.HTMLMediaElement>?mediaElement ^-> MediaElementAudioSourceNode
            |> WithComment "Creates a MediaElementAudioSourceNode given an HTMLMediaElement. As a consequence of calling this method, audio playback \
                            from the HTMLMediaElement will be re-routed into the processing graph of the AudioContext."
            "createMediaStreamSource" => T<JavaScript.MediaStream>?mediaStream ^-> MediaStreamAudioSourceNode
            "createMediaStreamDestination" => O ^-> MediaStreamAudioDestinationNode

            "createScriptProcessor" => (!? Ulong?bufferSize * !? Ulong?numberOfInputChannels * !? Ulong?numberOfOutputChannels) ^-> ScriptProcessorNode
            |> WithComment "Creates a ScriptProcessorNode for direct audio processing using JavaScript."
            "createAnalyser" => O ^-> AnalyserNode
            "createGain" => O ^-> GainNode
            "createDelay" => (!? T<double>?maxDelayTime) ^-> DelayNode
            |> WithComment "Creates a DelayNode representing a variable delay line. The initial default delay time will be 0 seconds."
            "createBiquadFilter" => O ^-> BiquadFilterNode
            |> WithComment "Creates a BiquadFilterNode representing a second order filter which can be configured as one of several common filter types."
            "createWaveShaper" => O ^-> WaveShaperNode
            |> WithComment "Creates a WaveShaperNode representing a non-linear distortion."
            "createPanner" => O ^-> PannerNode
            "createConvolver" => O ^-> ConvolverNode
            "createChannelSplitter" => (!? Ulong?numberOfOutputs) ^-> ChannelSplitterNode 
            |> WithComment "Creates an ChannelSplitterNode representing a channel splitter. An INDEX_SIZE_ERR exception must be thrown for invalid parameter values."
            "createChannelMerger" => (!? Ulong?numberOfInputs) ^-> ChannelMergerNode
            |> WithComment "Creates a ChannelMergerNode representing a channel merger. An INDEX_SIZE_ERR exception is thrown for invalid parameter values."

            "createDynamicCompressor" => O ^-> DynamicsCompressorNode
            "createOscillator" => O ^-> OscillatorNode
            "createPeriodicWave" => (T<JavaScript.Float32Array>?real * T<JavaScript.Float32Array>?imag) ^-> PeriodicWave
            |> WithComment "Creates a PeriodicWave representing a waveform containing arbitrary harmonic content. The real and imag parameters are of \
                            type Float32Array of equal lengths greater than zero and less than or equal to 4096 or an INDEX_SIZE_ERR exception must be thrown."
        ]

    let AudioNodeClass = 
        Class "AudioNode"
        |=> AudioNode
        |=> Inherits T<EventTarget>
        |+> Instance [
            "connect" => (AudioNode?destination * !? Ulong?output * !? Ulong?input) ^-> O
            |> WithComment "Connects the AudioNode to an AudioParam, controlling the parameter value with an audio-rate signal."
            "connect" => (AudioParam?destination * !? Ulong?output) ^-> O
            |> WithComment "Connects the AudioNode to an AudioParam, controlling the parameter value with an audio-rate signal."
            "disconnect" => (!? Ulong?output) ^-> O
            |> WithComment "Disconnects the AudioNode from the AudioParam it was connected to."

            "context" =? AudioContext
            |> WithComment "The AudioContext which owns this AudioNode."
            "numberOfInputs" =? Ulong
            |> WithComment "The number of inputs feeding into the AudioNode."
            "numberOfOutputs" =? Ulong
            |> WithComment "The number of outputs coming out of the AudioNode."

            "channelCount" =@ Ulong
            |> WithComment "The number of channels used when up-mixing and down-mixing connections to any inputs to the node."
            "channelCountMode" =@ ChannelCountMode
            |> WithComment "Determines how channels will be counted when up-mixing and down-mixing connections to any inputs to the node."
            "channelInterpretation" =@ ChannelInterpretation
            |> WithComment "Determines how individual channels will be treated when up-mixing and down-mixing connections to any inputs to the node."
        ]

    let ConvolverNodeClass = 
        Class "ConvolverNode"
        |=> Inherits AudioNode
        |=> ConvolverNode
        |+> Instance [
            "buffer" =@ AudioBuffer
            |> WithComment "A mono, stereo, or 4-channel AudioBuffer containing the (possibly multi-channel) impulse response used by the ConvolverNode."
            "normalize" =@ T<bool>
            |> WithComment "Controls whether the impulse response from the buffer will be scaled by an equal-power normalization when the buffer atttribute is set."
        ]
        
    let PanningModelType =
        Pattern.EnumStrings "PanningModelType" [
            "equalpower"
            "HRTF"
        ]

    let DistanceModelType =
        Pattern.EnumStrings "DistanceModelType" [
            "linear"
            "inverse"
            "exponential"
        ]

    let PannerNodeClass = 
        Class "PannerNode"
        |=> Inherits AudioNode
        |=> PannerNode
        |+> Instance [
            "panningModel" =@ PanningModelType
            |> WithComment "Specifies the panning model used by this PannerNode."

            "setPosition" => (T<double>?x * T<double>?y * T<double>?z) ^-> O
            |> WithComment "Sets the position of the audio source relative to the listener attribute. A 3D cartesian coordinate system is used."
            "setOrientation" => (T<double>?x * T<double>?y * T<double>?z) ^-> O
            |> WithComment "Describes which direction the audio source is pointing in the 3D cartesian coordinate space."
            "setVelocity" => (T<double>?x * T<double>?y * T<double>?z) ^-> O
            |> WithComment "Sets the velocity vector of the audio source. This vector controls both the direction of travel and the speed in 3D space."

            "distanceModel" =@ DistanceModelType
            |> WithComment "Specifies the distance model used by this PannerNode."
            "refDistance" =@ T<double>
            |> WithComment "A reference distance for reducing volume as source move further from the listener."
            "maxDistance" =@ T<double>
            |> WithComment "The maximum distance between source and listener, after which the volume will not be reduced any further."
            "rolloffFactor" =@ T<double>
            |> WithComment "Describes how quickly the volume is reduced as source moves away from listener."

            "coneInnerAngle" =@ T<double>
            |> WithComment "A parameter for directional audio sources, this is an angle in degrees, inside of which there will be no volume reduction."
            "coneOuterAngle" =@ T<double>
            |> WithComment "A parameter for directional audio sources, this is an angle in degrees, outside of which the volume will be reduced to a constant value of coneOuterGain."
            "coneOuterGain" =@ T<double>
            |> WithComment "A parameter for directional audio sources, this is the amount of volume reduction outside of the coneOuterAngle. "
        ]

    let AnalyserNodeClass =
        Class "AnalyserNode"
        |=> Inherits AudioNode
        |=> AnalyserNode
        |+> Instance [
            "getFloatFrequencyData" => T<JavaScript.Float32Array>?array ^-> O
            |> WithComment "Copies the current frequency data into the passed floating-point array."
            "getByteFrequencyData" => T<JavaScript.Uint8Array>?array ^-> O
            |> WithComment "Copies the current frequency data into the passed unsigned byte array."
            "getFloatTimeDomainData" => T<JavaScript.Float32Array>?array ^-> O
            |> WithComment "Copies the current time-domain (waveform) data into the passed floating-point array."
            "getByteTimeDomainData" => T<JavaScript.Uint8Array>?array ^-> O
            |> WithComment "Copies the current time-domain (waveform) data into the passed unsigned byte array."

            "fftSize" =@ Ulong
            |> WithComment "The size of the FFT used for frequency-domain analysis."
            "frequencyBinCount" =? Ulong
            |> WithComment "Half the FFT size."

            "minDecibels" =@ T<double>
            |> WithComment "The minimum power value in the scaling range for the FFT analysis data for conversion to unsigned byte values."
            "maxDecibels" =@ T<double>
            |> WithComment "The maximum power value in the scaling range for the FFT analysis data for conversion to unsigned byte values."

            "smoothingTimeConstant" =@ T<double>
            |> WithComment "A value from 0 -> 1 where 0 represents no time averaging with the last analysis frame."
        ]

    let ChannelSplitterNodeClass = 
        Class "ChannelSplitterNode"
        |=> Inherits AudioNode
        |=> ChannelSplitterNode

    let ChannelMergerNodeClass = 
        Class "ChannelMergerNode"
        |=> Inherits AudioNode
        |=> ChannelMergerNode

    let DynamicsCompressorNodeClass = 
        Class "DynamicsCompressorNode"
        |=> Inherits AudioNode
        |=> DynamicsCompressorNode
        |+> Instance [
            "threshold" =? AudioParam
            |> WithComment "The decibel value above which the compression will start taking effect."
            "knee" =? AudioParam
            |> WithComment "A decibel value representing the range above the threshold where the curve smoothly transitions to the ratio portion."
            "ratio" =? AudioParam
            |> WithComment "The amount of dB change in input for a 1 dB change in output."
            "reduction" =? AudioParam
            |> WithComment "Rpresents the current amount of gain reduction that the compressor is applying to the signal."
            "attack" =? AudioParam
            |> WithComment "The amount of time (in seconds) to reduce the gain by 10dB."
            "release" =? AudioParam
            |> WithComment "The amount of time (in seconds) to increase the gain by 10dB. "
        ] 

    let BiquadFilterType =
        Pattern.EnumStrings "BiquadFilterType" [
            "lowpass"
            "highpass"
            "bandpass"
            "lowshelf"
            "highshelf"
            "peaking"
            "notch"
            "allpass"
        ]

    let BiquadFilterNodeClass =
        Class "BiquadFilterNode"
        |=> Inherits AudioNode
        |=> BiquadFilterNode
        |+> Instance [
            "type" =@ BiquadFilterType
            |> WithComment "The type of this BiquadFilterNode."
            "frequency" =? AudioParam
            |> WithComment "The frequency at which the BiquadFilterNode will operate, in Hz."
            "detune" =? AudioParam
            |> WithComment "A detune value, in cents, for the frequency."
            "Q" =? AudioParam
            "gain" =? AudioParam

            "getFrequencyResponse" => (T<JavaScript.Float32Array>?frequencyHz * T<JavaScript.Float32Array>?magResponse * T<JavaScript.Float32Array>?phaseResponse) ^-> O
            |> WithComment "Given the current filter parameter settings, calculates the frequency response for the specified frequencies."
        ]

    let OverSampleType =
        Pattern.EnumStrings "OverSampleType" [
            "none"
            "2x"
            "4x"
        ]

    let WaveShaperNodeClass = 
        Class "WaveShaperNode"
        |=> Inherits AudioNode
        |=> WaveShaperNode
        |+> Instance [
            "curve" =@ T<JavaScript.Float32Array>
            |> WithComment "The shaping curve used for the waveshaping effect. The input signal is nominally within the range -1 -> +1."
            "oversample" =@ OverSampleType
            |> WithComment "Specifies what type of oversampling (if any) should be used when applying the shaping curve."
        ]

    let OscillatorType =
        Pattern.EnumStrings "OscillatorType" [
            "sine"
            "square"
            "sawtooth"
            "triangle"
            "custom"
        ]

    let OscillatorNodeClass = 
        Class "OscillatorNode"
        |=> Inherits AudioNode
        |=> OscillatorNode
        |+> Instance [
            "type" =@ OscillatorType
            |> WithComment "The shape of the periodic waveform."
            "frequency" =? AudioParam
            |> WithComment "The frequency (in Hertz) of the periodic waveform."
            "detune" =? AudioParam
            |> WithComment "A detuning value (in Cents) which will offset the frequency by the given amount."

            "start" => T<double>?``when`` ^-> O
            |> WithComment "Schedules a sound to playback at an exact time." 
            "stop" => T<double>?``when`` ^-> O
            |> WithComment "Schedules a sound to stop playback at an exact time."
            "setPeriodicWave" => PeriodicWave?periodicWave ^-> O
            |> WithComment "Sets an arbitrary custom periodic waveform given a PeriodicWave."

            "onended" =@ Event?event ^-> O
        ]

    let MediaStreamAudioSourceNodeClass =
        Class "MediaStreamAudioSourceNode" 
        |=> Inherits AudioNode
        |=> MediaStreamAudioSourceNode


    let MediaElementAudioSourceNodeClass = 
        Class "MediaElementAudioSourceNode"
        |=> Inherits AudioNode

    let MediaStreamAudioDestinationNodeClass = 
        Class "MediaStreamAudioDestinationNode"
        |=> Inherits AudioNode
        |=> MediaStreamAudioDestinationNode
        |+> Instance [
            "stream" =@ T<JavaScript.MediaStream>
            |> WithComment "A MediaStream containing a single AudioMediaStreamTrack with the same number of channels as the node itself."
        ]

    let OfflineAudioContext =
        Class "OfflineAudioContext"
        |=> Inherits AudioContext
        |+> Static [
            Constructor (Ulong?numberOfChannels * Ulong?length * T<float>?sampleRate)
        ]
        |+> Instance [
            "startRendering" => O ^-> O
            |> WithComment "Given the current connections and scheduled changes, starts rendering audio."
            //"startRendering" => O ^-> Promise<AudioBuffer>
            "oncomplete" =@ OfflineAudioCompletitionEvent ^-> O
        ]

    let Assembly = 
        Assembly [
            Namespace "IntelliFactory.WebSharper.JavaScript" [
                OfflineAudioCompletitionEvent
                AudioDestinationNodeClass
                AudioListener
                AudioBufferClass
                AudioParam
                AudioBufferSourceNode
                GainNode
                DelayNode
                PeriodicWave
                ChannelCountMode
                ChannelInterpretation
                AudioProcessingEvent
                ScriptProcessorNode
                AudioContext
                AudioNodeClass
                ConvolverNodeClass
                PanningModelType
                DistanceModelType
                PannerNodeClass
                AnalyserNodeClass
                ChannelSplitterNodeClass
                ChannelMergerNodeClass
                DynamicsCompressorNodeClass
                BiquadFilterType
                BiquadFilterNodeClass
                OverSampleType
                WaveShaperNodeClass
                OscillatorType
                OscillatorNodeClass
                MediaStreamAudioSourceNodeClass
                MediaElementAudioSourceNodeClass
                MediaStreamAudioDestinationNodeClass
                OfflineAudioContext
            ]
        ] 

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()

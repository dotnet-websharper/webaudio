// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2018 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}
namespace WebAudio

open WebSharper.InterfaceGenerator

module Definition =
    open WebSharper
    open WebSharper.JavaScript.Dom

    let O = T<unit>
    let Error = T<exn>
    let Ulong = T<int>
    let Event = T<Event>

    let AudioNode = Class "AudioNode"
    let AudioScheduledSourceNode =
        Class "AudioScheduledSourceNode"
        |=> Inherits AudioNode
        |+> Instance [
            "start" => !?Ulong?``when`` ^-> O

            "stop" => !?Ulong?``when`` ^-> O

            "onended" =@ Event ^-> O |> WithSourceName "OnEnded"
        ]
    let AudioDestinationNode = Class "AudioDestinationNode" 
    let PannerNode = Class "PannerNode"
    let MediaElementAudioSourceNode = Class "MediaElementAudioSourceNode" 
    let ConvolverNode = Class "ConvolverNode" 
    let AnalyserNode = Class "AnalyserNode"
    let ChannelSplitterNode = Class "ChannelSplitterNode"
    let ChannelMergerNode = Class "ChannelMergerNode"
    let DynamicsCompressorNode = Class "DynamicsCompressorNode"
    let IIRFilterNode = Class "IIRFilterNode"

    let BiquadFilterNode = Class "BiquadFilterNode"
    let WaveShaperNode = Class "WaveShaperNode"
    let OscillatorNode = Class "OscillatorNode"
    let MediaStreamAudioSourceNode = Class "MediaStreamAudioSourceNode"
    let MediaStreamAudioDestinationNode = Class "MediaStreamAudioDestinationNode" 
    let MediaStreamTrackAudioSourceNode = 
        Class "MediaStreamTrackAudioSourceNode"
        |=> Inherits AudioNode
    let StereoPannerNode =
        Class "StereoPannerNode"
        |=> Inherits AudioNode
    
    let AudioBuffer = Class "AudioBuffer"

    let ConstantSourceNode = 
        Class "ConstantSourceNode"
        |=> Inherits AudioScheduledSourceNode

    

    let AudioWorklet = 
        Class "AudioWorklet"


    let OfflineAudioCompletionEvent = 
        Class "OfflineAudioCompletionEvent"
        |=> Inherits Event
        |+> Instance [
            "renderedBuffer" =? AudioBuffer
        ]

    AudioDestinationNode
        |=> Inherits AudioNode
        |+> Instance [
            "maxChannelCount" =? Ulong
        ]
        |> ignore

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

    AudioBuffer
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
        |> ignore

    let AudioParam =
        Class "AudioParam"
        |+> Instance [
            "value" =@ T<float>
            |> WithComment "The parameter's floating-point value."
            "defaultValue" =? T<float>
            |> WithComment "Initial value for the value attribute."
            "maxValue" =? T<float>
            |> WithComment "Represents the maximum possible value for the parameter's nominal (effective) range."
            "minValue" =? T<float>
            |> WithComment "Represents the minimum possible value for the parameter's nominal (effective) range."


            "setValueAtTime" => (T<float>?value * T<double>?startTime) ^-> TSelf
            |> WithComment "Schedules a parameter value change at the given time."
            "linearRampToValueAtTime" => (T<float>?value * T<double>?endTime) ^-> TSelf
            |> WithComment "Schedules a linear continuous change in parameter value from the previous scheduled parameter value to the given value."
            "exponentialRampToValueAtTime" => (T<float>?value * T<double>?endTime) ^-> TSelf
            |> WithComment "Schedules an exponential continuous change in parameter value from the previous scheduled parameter value to the given value."

            "setTargetAtTime" => (T<float>?target * T<double>?startTime * T<double>?timeConstant) ^-> TSelf
            |> WithComment "Start exponentially approaching the target value at the given time with a rate having the given time constant."
            "setValueCurveAtTime" => (T<JavaScript.Float32Array>?values * T<double>?startTime * T<double>?duration) ^-> TSelf
            |> WithComment "Sets an array of arbitrary parameter values starting at the given time for the given duration."
            "cancelScheduledValues" => T<double>?startTime ^-> TSelf
            |> WithComment "Cancels all scheduled parameter changes with times greater than or equal to startTime."
            "cancelAndHoldAtTime" => T<double>?cancelTime ^-> TSelf
        ]

    ConstantSourceNode
    |+> Instance [
        "offset" =@ AudioParam
    ] 
    |> ignore

    let WorkletGlobalScope = Class "WorkletGlobalScope"
    let AudioWorkletGlobalScope =
        Class "AudioWorkletGlobalScope"
        |=> Inherits WorkletGlobalScope
        |+> Instance [
            "currentFrame" =? T<int>
            "currentTime" =? T<double>
            "sampleRate" =? T<float>

            "registerProcessor" => T<string>?name * T<obj>?processorCtor ^-> O // TODO: check this shit out
        ]

    let AudioBufferSourceNode = 
        Class "AudioBufferSourceNode"
        |=> Inherits AudioScheduledSourceNode
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
    let BaseAudioContext =
        let DecodesuccessCallback = AudioBuffer ^-> O 
        let ErrorCallback = O ^-> O
        let AudioContextState = Pattern.EnumStrings "AudioContextState" [
            "suspended"; "running"; "closed"
        ]
        Class "BaseAudioContext" // previous one
        |=> Inherits T<EventTarget>
        |=> Nested [AudioContextState]
        |+> Instance [

            "audioWorklet" =? AudioWorklet
            "state" =? AudioContextState

            "destination" =? AudioDestinationNode
            |> WithComment "An AudioDestinationNode with a single input representing the final destination for all audio."
            "sampleRate" =? T<float>
            |> WithComment "The sample rate (in sample-frames per second) at which the AudioContext handles audio."
            "currentTime" =? T<double>
            |> WithComment "This is a time in seconds which starts at zero when the context is created and increases in real-time."
            "listener" =? AudioListener
            |> WithComment "An AudioListener which is used for 3D spatialization."

            "createConstantSource" => O ^-> ConstantSourceNode
            |> WithComment "Creates a ConstantSourceNode object, which is an audio source that continuously outputs a monaural (one-channel) sound signal\
                            whose samples all have the same value."
            "createIIRFilter" => (!|T<float>)?feedForward * (!|T<float>)?feedback ^-> IIRFilterNode
            |> WithComment "Creates an IIRFilterNode, which represents a second order filter configurable as several different common filter types.\
                            Both feedForward and feedback may have up to 20 members, the first of which must not be zero."

            "createStereoPanner" => O ^-> StereoPannerNode
            |> WithComment "Creates a StereoPannerNode, which can be used to apply stereo panning to an audio source."
            
            "createBuffer" => (Ulong?numberOfChannels * Ulong?length * T<float>?sampleRate) ^-> AudioBuffer
            |> WithComment "Creates an AudioBuffer of the given size. The audio data in the buffer will be zero-initialized (silent).\
                            An NOT_SUPPORTED_ERR exception is thrown if any of the arguments is negative, zero, or outside its nominal range."
            "decodeAudioData" => (T<JavaScript.ArrayBuffer>?audioData * DecodesuccessCallback?successCallback * !? ErrorCallback?errorCallback) ^-> O
            |> WithComment "Asynchronously decodes the audio file data contained in the ArrayBuffer."
            //"decodeAudioData" => (T<JavaScript.ArrayBuffer>?audioData * !? DecodesuccessCallback?successCallback * !? ErrorCallback?errorCallback) ^-> Promise<AudioBuffer>

            "createBufferSource" => O ^-> AudioBufferSourceNode

            "createScriptProcessor" => (!? Ulong?bufferSize * !? Ulong?numberOfInputChannels * !? Ulong?numberOfOutputChannels) ^-> ScriptProcessorNode
            |> WithComment "Creates a ScriptProcessorNode for direct audio processing using JavaScript."
            |> ObsoleteWithMessage "This feature was replaced by AudioWorklets and AudioWorkletNode."
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

            "createDynamicsCompressor" => O ^-> DynamicsCompressorNode
            "createOscillator" => O ^-> OscillatorNode
            "createPeriodicWave" => (T<JavaScript.Float32Array>?real * T<JavaScript.Float32Array>?imag) ^-> PeriodicWave
            |> WithComment "Creates a PeriodicWave representing a waveform containing arbitrary harmonic content. The real and imag parameters are of \
                            type Float32Array of equal lengths greater than zero and less than or equal to 4096 or an INDEX_SIZE_ERR exception must be thrown."
        
            "onstatechange" => Event ^-> O
        ]
    let AudioContext = 
        let AudioTimestamp = 
            Class "AudioTimestamp"
            |+> Instance [
                "contextTime" =? T<float>
                "performanceTime" =? T<float>
            ]
        Class "AudioContext"
        |=> Inherits BaseAudioContext
        |=> Nested [AudioTimestamp]
        |+> Static [
            Constructor O 
            |> WithInline "new (window.AudioContext || window.webkitAudioContext)()"
        ]
        |+> Instance [
            "baseLatency" =? T<double>
            "outputLatency" =? T<double>
            "sinkId" =? T<string>


            "close" => O ^-> T<JavaScript.Promise<unit>> |> WithComment "Causes an INVALID_STATE_ERR exception to be thrown if called on an OfflineAudioContext. "
            "createMediaElementSource" => T<JavaScript.HTMLMediaElement> ^-> MediaElementAudioSourceNode
            |> WithComment "Creates a MediaElementAudioSourceNode given an HTMLMediaElement. As a consequence of calling this method, audio playback \
                            from the HTMLMediaElement will be re-routed into the processing graph of the AudioContext."
            "createMediaStreamDestination" => O ^-> MediaStreamAudioDestinationNode
            "createMediaStreamSource" => T<JavaScript.MediaStream> ^-> MediaStreamAudioSourceNode
            "createMediaStreamTrackSource" => T<JavaScript.MediaStreamTrack> ^-> MediaStreamTrackAudioSourceNode
            "getOutputTimestamp" => O ^-> AudioTimestamp
            "resume" => O ^-> T<JavaScript.Promise<unit>> |> WithComment "Causes an INVALID_STATE_ERR exception to be thrown if called on an OfflineAudioContext. "
            "setSinkId" => (T<string> + T<obj>(*todo AudioSinkInfo*)) ^-> O
            "suspend" => O ^-> T<JavaScript.Promise<unit>> |> WithComment "Causes an INVALID_STATE_ERR exception to be thrown if called on an OfflineAudioContext. "

            "onsinkchange" =@ T<Event> ^-> O |> WithSourceName "OnSinkChange"



            
        ]

            
    IIRFilterNode
    |=> Inherits AudioNode
    |+> Instance [
        let f32arr = T<JavaScript.Float32Array>
        "getFrequencyResponse" => f32arr?frequencyArray * f32arr?magResponseOutput * f32arr?phaseResponseOutput ^-> O
    ]
    |> ignore


    MediaStreamTrackAudioSourceNode
    |+> Static [
        Constructor (AudioContext?context * T<obj>?options)
    ]
    |> ignore


    let AudioWorkletNode =
        let AudioWorkletNodeOptions =
            Pattern.Config "AudioWorkletNode.AudioWorkletOptions" {
                Required = []
                Optional = [
                    "numberOfInputs", Ulong
                    "numberOfOutputs", Ulong
                    "outputChannelCount", !|Ulong
                    "parameterData", !|AudioParam
                    "processorOptions", T<obj>
                ]
            }
        Class "AudioWorkletNode"
        |=> Inherits AudioNode
        |=> Nested [AudioWorkletNodeOptions]
        |+> Static [
            Constructor (BaseAudioContext?context * T<string>?name * !?AudioWorkletNodeOptions?options)
        ]
        |+> Instance [
            "port" =? T<obj> // TODO: bind MessagePort 
            "parameters" =? T<System.Collections.Generic.Dictionary<_,_>>[T<string>,AudioParam]
            "onprocessorerror" =@ Event ^-> O |> WithSourceName "OnProcessorError"
        ]

    let AudioWorkletProcessor =
        AbstractClass "AudioWorkletProcessor"
        |+> Static [
            Constructor O
        ]
        |+> Instance [
            "port" =? T<obj> // TODO: bind MessagePort 
            // TODO: The AudioWorkletProcessor interface does not define any methods of its own. However, you must provide a process() method, which is called in order to process the audio stream.
        ]

    AudioNode
        |=> Inherits T<EventTarget>
        |+> Instance [
            "connect" => ((AudioNode + AudioParam)?destination * !? Ulong?outputIndex * !? Ulong?inputIndex) ^-> O
            |> WithComment "Connects the AudioNode to an AudioParam, controlling the parameter value with an audio-rate signal."
            "disconnect" => (!?(AudioNode + AudioParam)?destination * !? Ulong?output * !?Ulong?input) ^-> O
            |> WithComment "Disconnects the AudioNode from the AudioParam it was connected to."

            "context" =? BaseAudioContext
            |> WithComment "The AudioContext or OfflineAudioContext which owns this AudioNode."
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
        |> ignore

    ConvolverNode
        |=> Inherits AudioNode
        |+> Instance [
            "buffer" =@ AudioBuffer
            |> WithComment "A mono, stereo, or 4-channel AudioBuffer containing the (possibly multi-channel) impulse response used by the ConvolverNode."
            "normalize" =@ T<bool>
            |> WithComment "Controls whether the impulse response from the buffer will be scaled by an equal-power normalization when the buffer atttribute is set."
        ]
        |> ignore
        
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

    
    PannerNode
        |=> Inherits AudioNode
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
        |> ignore

    AnalyserNode
        |=> Inherits AudioNode
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
        |> ignore

    ChannelSplitterNode 
        |=> Inherits AudioNode
        |> ignore

    ChannelMergerNode
        |=> Inherits AudioNode
        |> ignore

    DynamicsCompressorNode
        |=> Inherits AudioNode
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
        |> ignore

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

    BiquadFilterNode
        |=> Inherits AudioNode
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
        |> ignore

    let OverSampleType =
        Pattern.EnumStrings "OverSampleType" [
            "none"
            "2x"
            "4x"
        ]

    WaveShaperNode
        |=> Inherits AudioNode
        |+> Instance [
            "curve" =@ T<JavaScript.Float32Array>
            |> WithComment "The shaping curve used for the waveshaping effect. The input signal is nominally within the range -1 -> +1."
            "oversample" =@ OverSampleType
            |> WithComment "Specifies what type of oversampling (if any) should be used when applying the shaping curve."
        ]
        |> ignore

    let OscillatorType =
        Pattern.EnumStrings "OscillatorType" [
            "sine"
            "square"
            "sawtooth"
            "triangle"
            "custom"
        ]

    OscillatorNode
        |=> Inherits AudioNode
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
        |> ignore

    MediaStreamAudioSourceNode
        |=> Inherits AudioNode
        |> ignore 


    let MediaElementAudioSourceNodeClass = 
        Class "MediaElementAudioSourceNode"
        |=> Inherits AudioNode

    MediaStreamAudioDestinationNode
        |=> Inherits AudioNode
        |+> Instance [
            "stream" =@ T<JavaScript.MediaStream>
            |> WithComment "A MediaStream containing a single AudioMediaStreamTrack with the same number of channels as the node itself."
        ]
        |> ignore

    let OfflineAudioContext =
        Class "OfflineAudioContext"
        |=> Inherits BaseAudioContext
        |+> Static [
            Constructor (Ulong?numberOfChannels * Ulong?length * T<float>?sampleRate)
        ]
        |+> Instance [
            "startRendering" => O ^-> O
            |> WithComment "Given the current connections and scheduled changes, starts rendering audio."

            "suspend" => T<float>?suspendTime ^-> T<JavaScript.Promise<unit>>
            "resume" => O ^-> T<JavaScript.Promise<unit>>
            //"startRendering" => O ^-> Promise<AudioBuffer>
            "oncomplete" =@ OfflineAudioCompletionEvent ^-> O
        ]

    let Assembly = 
        Assembly [
            Namespace "WebSharper.JavaScript" [
                OfflineAudioCompletionEvent
                WorkletGlobalScope
                AudioWorklet
                AudioWorkletGlobalScope
                AudioWorkletProcessor
                AudioDestinationNode
                AudioListener
                AudioBuffer
                AudioParam
                AudioBufferSourceNode
                GainNode
                DelayNode
                PeriodicWave
                ChannelCountMode
                ChannelInterpretation
                AudioProcessingEvent
                StereoPannerNode
                ConstantSourceNode
                ScriptProcessorNode
                AudioContext
                BaseAudioContext
                AudioNode
                AudioScheduledSourceNode
                AudioWorkletNode
                ConvolverNode
                PanningModelType
                DistanceModelType
                IIRFilterNode
                PannerNode
                AnalyserNode
                ChannelSplitterNode
                ChannelMergerNode
                DynamicsCompressorNode
                BiquadFilterType
                BiquadFilterNode
                OverSampleType
                WaveShaperNode
                OscillatorType
                OscillatorNode
                MediaStreamAudioSourceNode
                MediaElementAudioSourceNode
                MediaStreamAudioDestinationNode
                MediaStreamTrackAudioSourceNode
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

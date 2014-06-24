namespace WebAudio

open IntelliFactory.WebSharper.InterfaceGenerator

module Definition =
    open IntelliFactory.WebSharper.Html5
    open IntelliFactory.WebSharper.Dom

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
        |+> Protocol [
            "renderedBuffer" =? AudioBuffer
        ]

    let AudioDestinationNodeClass =
        Class "AudioDestinationNode"
        |=> Inherits AudioNode
        |=> AudioDestinationNode
        |+> Protocol [
            "maxChannelCount" =? Ulong
        ]

    let AudioListener = 
        Class "AudioListener"
        |+> Protocol [
            "dopplerFactor" =@ T<double>
            "speedOfSound" =@ T<double>

            "setPosition" => (T<double>?x * T<double>?y * T<double>?z) ^-> O
            "setOrientation" => (T<double>?x * T<double>?y * T<double>?z * T<double>?xUp * T<double>?yUp * T<double>?zUp) ^-> O
            //"setVelocity" => (T<double>?x * T<double>?y * T<double>?z) ^-> O
        ]

    let AudioBufferClass = 
        Class "AudioBuffer"
        |=> AudioBuffer
        |+> Protocol [
            "sampleRate" =? T<float>
            "length" =? T<int>

            "duration" =? T<double>
            "numberOfChannels" =? T<int>

            "getChannelData" => Ulong?channel ^-> T<Float32Array>

            "copyFromChannel" => (T<Float32Array>?destination * T<int>?channelNumber * !? Ulong?startInChannel) ^-> O
            "copyToChannel" => (T<Float32Array>?source * T<int>?channelNumber * !? Ulong?startInChannel) ^-> O
        ]

    let AudioParam =
        Class "AudioParam"
        |+> Protocol [
            "value" =@ T<float>
            "defaultValue" =? T<float>

            "setValueAtTime" => (T<float>?value * T<double>?startTime) ^-> O
            "linearRampToValueAtTime" => (T<float>?value * T<double>?endTime) ^-> O
            "exponentialRampToValueAtTime" => (T<float>?value * T<double>?endTime) ^-> O

            "setTargetAtTime" => (T<float>?target * T<double>?startTime * T<double>?timeConstant) ^-> O
            "setValueCurveAtTime" => (T<Float32Array>?values * T<double>?startTime * T<double>?duration) ^-> O
            "cancelScheduledValues" => T<double>?startTime ^-> O
        ]

    let AudioBufferSourceNode = 
        Class "AudioBufferSourceNode"
        |=> Inherits AudioNode
        |+> Protocol [
            "buffer" =@ AudioBuffer
            "playbackRate" =? AudioParam

            "loop" =@ T<bool>
            "loopStart" =@ T<double>
            "loopEnd" =@ T<double>

            "start" => (!? T<double>?``when`` * !? T<double>?offset * !? T<double>?duration) ^-> O
            "stop" => (!? T<double>?``when``) ^-> O

            "onended" =@ Event?event ^-> O
        ]

    let GainNode = 
        Class "GainNode"
        |=> Inherits AudioNode
        |+> Protocol [
            "gain" =? AudioParam
        ]

    let DelayNode = 
        Class "DelayNode"
        |=> Inherits AudioNode
        |+> Protocol [
            "delayTime" =? AudioParam
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
        |+> Protocol [
            "playbackTime" =? T<double>
            "inputBuffer" =? AudioBuffer
            "outputBuffer" =? AudioBuffer
        ]

    let ScriptProcessorNode = 
        Class "ScriptProcessorNode"
        |=> Inherits AudioNode
        |+> Protocol [
            "onaudioprocess" =@ AudioProcessingEvent ^-> O 

            "bufferSize" =? T<int>
        ]

    let AudioContext = 
        let DecodesuccessCallback = AudioBuffer ^-> O 
        let ErrorCallback = O ^-> O

        Class "AudioContext"
        |=> Inherits T<EventTarget>
        |+> [ Constructor O ]
        |+> Protocol [
            "destination" =? AudioDestinationNode
            "sampleRate" =? T<float>
            "currentTime" =? T<double>
            "listener" =? AudioListener
            
            "createBuffer" => (Ulong?numberOfChannels * Ulong?length * T<float>?sampleRate) ^->AudioBuffer
            "decodeAudioData" => (T<ArrayBuffer>?audioData * DecodesuccessCallback?successCallback * !? ErrorCallback?errorCallback) ^-> O
            //"decodeAudioData" => (T<ArrayBuffer>?audioData * !? DecodesuccessCallback?successCallback * !? ErrorCallback?errorCallback) ^-> Promise<AudioBuffer>

            "createBufferSource" => O ^-> AudioBufferSourceNode
            "createMediaElementSource" => T<HTMLMediaElement>?mediaElement ^-> MediaElementAudioSourceNode
            "createMediaStreamSource" => T<MediaStream>?mediaStream ^-> MediaStreamAudioSourceNode
            "createMediaStreamDestination" => O ^-> MediaStreamAudioDestinationNode

            "createScriptProcessor" => (!? Ulong?bufferSize * !? Ulong?numberOfInputChannels * !? Ulong?numberOfOutputChannels) ^-> ScriptProcessorNode

            "createAnalyser" => O ^-> AnalyserNode
            "createGain" => O ^-> GainNode
            "createDelay" => (!? T<double>?maxDelayTime) ^-> DelayNode
            "createBiquadFilter" => O ^-> BiquadFilterNode
            "createWaveShaper" => O ^-> WaveShaperNode
            "createPanner" => O ^-> PannerNode
            "createConvolver" => O ^-> ConvolverNode
            "createChannelSplitter" => (!? Ulong?numberOfOutputs) ^-> ChannelSplitterNode 
            "createChannelMerger" => (!? Ulong?numberOfInputs) ^-> ChannelMergerNode

            "createDynamicCompressor" => O ^-> DynamicsCompressorNode
            "createOscillator" => O ^-> OscillatorNode
            "createPeriodicWave" => (T<Float32Array>?real * T<Float32Array>?imag) ^-> PeriodicWave
        ]

    let AudioNodeClass = 
        Class "AudioNode"
        |=> AudioNode
        |=> Inherits T<EventTarget>
        |+> Protocol [
            "connect" => (AudioNode?destination * !? Ulong?output * !? Ulong?input) ^-> O
            "connect" => (AudioParam?destination * !? Ulong?output) ^-> O
            "disconnect" => (!? Ulong?output) ^-> O

            "context" =? AudioContext
            "numberOfInputs" =? Ulong
            "numberOfOutputs" =? Ulong

            "channelCount" =@ Ulong
            "channelCountMode" =@ ChannelCountMode
            "channelInterpretation" =@ ChannelInterpretation
        ]

    let ConvolverNodeClass = 
        Class "ConvolverNode"
        |=> Inherits AudioNode
        |=> ConvolverNode
        |+> Protocol [
            "buffer" =@ AudioBuffer
            "normalize" =@ T<bool>
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
        |+> Protocol [
            "panningModel" =@ PanningModelType

            "setPosition" => (T<double>?x * T<double>?y * T<double>?z) ^-> O
            "setOrientation" => (T<double>?x * T<double>?y * T<double>?z) ^-> O
            "setVelocity" => (T<double>?x * T<double>?y * T<double>?z) ^-> O

            "distanceModel" =@ DistanceModelType
            "refDistance" =@ T<double>
            "maxDistance" =@ T<double>
            "rolloffFactor" =@ T<double>

            "coneInnerAngle" =@ T<double>
            "coneOuterAngle" =@ T<double>
            "coneOuterGain" =@ T<double>
        ]

    let AnalyserNodeClass =
        Class "AnalyserNode"
        |=> Inherits AudioNode
        |=> AnalyserNode
        |+> Protocol [
            "getFloatFrequencyData" => T<Float32Array>?array ^-> O
            "getByteFrequencyData" => T<Uint8Array>?array ^-> O
            "getFloatTimeDomainData" => T<Float32Array>?array ^-> O
            "getByteTimeDomainData" => T<Uint8Array>?array ^-> O

            "fftSize" =@ Ulong
            "frequencyBinCount" =? Ulong

            "minDecibels" =@ T<double>
            "maxDecibels" =@ T<double>

            "smoothingTimeConstant" =@ T<double>
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
        |+> Protocol [
            "threshold" =? AudioParam
            "knee" =? AudioParam
            "ratio" =? AudioParam
            "reduction" =? AudioParam
            "attack" =? AudioParam
            "release" =? AudioParam
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
        |+> Protocol [
            "type" =@ BiquadFilterType
            "frequency" =? AudioParam
            "detune" =? AudioParam
            "Q" =? AudioParam
            "gain" =? AudioParam

            "getFrequencyResponse" => (T<Float32Array>?frequencyHz * T<Float32Array>?magResponse * T<Float32Array>?phaseResponse) ^-> O
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
        |+> Protocol [
            "curve" =@ T<Float32Array>
            "oversample" =@ OverSampleType
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
        |+> Protocol [
            "type" =@ OscillatorType
            "frequency" =? AudioParam
            "detune" =? AudioParam

            "start" => T<double>?``when`` ^-> O
            "stop" => T<double>?``when`` ^-> O
            "setPeriodicWave" => PeriodicWave?periodicWave ^-> O

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
        |+> Protocol [
            "stream" =@ T<MediaStream>
        ]

    let OfflineAudioContext =
        Class "OfflineAudioContext"
        |=> Inherits AudioContext
        |+> [
            Constructor (Ulong?numberOfChannels * Ulong?length * T<float>?sampleRate)
        ]
        |+> Protocol [
            "startRendering" => O ^-> O
            //"startRendering" => O ^-> Promise<AudioBuffer>
            "oncomplete" =@ OfflineAudioCompletitionEvent ^-> O
        ]

    let Assembly = 
        Assembly [
            Namespace "IntelliFactory.WebSharper.Html5" [
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